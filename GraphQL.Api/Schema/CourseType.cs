namespace GraphQL.Api.Schema;

public class CourseType
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Salary { get; set; }
    
    [GraphQLNonNullType]
    public TeacherType Teacher { get; set; }
    public IEnumerable<StudentType> Students { get; set; }
}