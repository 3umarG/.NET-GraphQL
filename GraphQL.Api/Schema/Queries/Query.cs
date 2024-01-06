namespace GraphQL.Api.Schema;

public class Query
{
    [GraphQLDeprecated("Not Supported yet ..")]
    public string Instructions => "Hello there ...!!";

    public Task<IEnumerable<CourseType>> GetCourses()
    {
        var courses = new List<CourseType>();
        for (var i = 0; i < 5; i++)
        {
            courses.Add(new CourseType
            {
                Id = i+1,
                Name = $"Course Number : {i+1}",
                Salary = i * 100 + 30,
                Students = new []
                {
                    new StudentType
                    {
                        Id = i + 2,
                        FirstName = $"Student : {i+1}",
                        LastName = "Last Name",
                        GPA = 4
                    }
                },
                Teacher = new TeacherType
                {
                    Id = 2,
                    FirstName = $"Teacher : {i+1}",
                    LastName = "Last Name",
                    Salary = 2000
                }
            });
        }

        return Task.FromResult<IEnumerable<CourseType>>(courses);
    }


    public async Task<StudentType> GetStudentByIdAsync(int id)
    {
        await Task.Delay(100);
        return new StudentType
        {
            Id = id,
            FirstName = $"Student with ID : {id}",
            LastName = "Last Name",
            GPA = 4
        };
    }
}