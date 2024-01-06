namespace GraphQL.Api.Models;

public class Teacher
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public double Salary { get; set; }

    public IEnumerable<Course> Courses { get; set; }
}