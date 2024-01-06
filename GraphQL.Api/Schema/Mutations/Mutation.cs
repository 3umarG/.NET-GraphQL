using GraphQL.Api.Schema.Subscriptions;
using HotChocolate.Subscriptions;

namespace GraphQL.Api.Schema.Mutations;

public class Mutation
{
    private readonly List<CourseType> _courses;

    public Mutation()
    {
        _courses = new List<CourseType>();
    }

    public async Task<CourseType> CreateCourse(CourseInputType courseInput,
        [Service] ITopicEventSender topicEventSender)
    {
        var course = new CourseType
        {
            Id = courseInput.Id,
            Name = courseInput.Name,
            Price = courseInput.Price,
            Teacher = new TeacherType
            {
                Id = courseInput.TeacherId
            }
        };

        _courses.Add(course);

        await topicEventSender.SendAsync(nameof(Subscription.CourseCreated), course);
        return course;
    }


    public CourseType UpdateCourse(int id, CourseInputType courseInput)
    {
        var course = _courses.FirstOrDefault(c => c.Id == id);
        if (course is null)
        {
            throw new GraphQLException(new Error($"Not Found Course with ID : {id}", code: "404"));
        }

        course.Id = courseInput.Id;
        course.Name = courseInput.Name;
        course.Price = courseInput.Price;
        course.Teacher.Id = courseInput.TeacherId;

        return course;
    }
}