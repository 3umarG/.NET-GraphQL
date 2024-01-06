using GraphQL.Api.DataLoaders;

namespace GraphQL.Api.Schema.Queries;

public class CourseType
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public int TeacherId { get; set; }

    [GraphQLNonNullType]
    public async Task<TeacherType> Teacher([Service] TeacherDataLoader teacherDataLoader)
    {
        var teacher = await teacherDataLoader.LoadAsync(TeacherId);
        return new TeacherType
        {
            Id = teacher.Id,
            Salary = teacher.Salary,
            FirstName = teacher.FirstName,
            LastName = teacher.LastName
        };
    }

    public IEnumerable<StudentType> Students { get; set; }
}