namespace GraphQL.Api.Models;

public class Course
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }

    public int TeacherId { get; set; }
    public Teacher Teacher { get; set; }

    public ICollection<Student> Students { get; set; } = new HashSet<Student>();
}