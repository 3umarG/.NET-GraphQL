namespace GraphQL.Api.Models;

public class Student
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public double GPA { get; set; }

    public ICollection<Course> Courses { get; set; } = new HashSet<Course>();
}