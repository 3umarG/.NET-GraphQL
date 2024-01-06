namespace GraphQL.Api.Schema.Queries;

public class CourseType
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    
    [GraphQLNonNullType]
    public TeacherType Teacher { get; set; }
    public IEnumerable<StudentType> Students { get; set; }
}