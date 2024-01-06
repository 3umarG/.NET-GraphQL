namespace GraphQL.Api.Schema.Mutations;

public class CourseInputType
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public int TeacherId { get; set; }
}