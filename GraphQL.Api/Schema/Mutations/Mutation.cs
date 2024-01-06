using GraphQL.Api.Models;
using GraphQL.Api.Repositories;
using GraphQL.Api.Schema.Queries;
using GraphQL.Api.Schema.Subscriptions;
using HotChocolate.Subscriptions;

namespace GraphQL.Api.Schema.Mutations;

public class Mutation
{
    private readonly CoursesRepository _coursesRepository;

    public Mutation(CoursesRepository coursesRepository)
    {
        this._coursesRepository = coursesRepository;
    }


    public async Task<CourseType> CreateCourse(CourseInputType courseInput,
        [Service] ITopicEventSender topicEventSender)
    {
        try
        {
            var course = await _coursesRepository.Create(courseInput);

            var courseType = MapCourseTypeFromCourseModel(course);

            await topicEventSender.SendAsync(nameof(Subscription.CourseCreated), course);
            return courseType;
        }
        catch (Exception e)
        {
            throw new GraphQLException(new Error(e.Message));
        }
    }

    private static CourseType MapCourseTypeFromCourseModel(Course course)
    {
        return new CourseType
        {
            Id = course.Id,
            Name = course.Name,
            Price = course.Price,
            TeacherId = course.TeacherId
        };
    }


    public async Task<CourseType> UpdateCourse(int id, CourseInputType courseInput)
    {
        var course = await _coursesRepository.Update(id, courseInput);

        var courseType = MapCourseTypeFromCourseModel(course);

        return courseType;
    }


    public async Task<CourseType> DeleteCourse(int id)
    {
        var course = await _coursesRepository.DeleteById(id);

        return MapCourseTypeFromCourseModel(course);
    }
}