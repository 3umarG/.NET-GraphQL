using GraphQL.Api.Models;
using GraphQL.Api.Repositories;

namespace GraphQL.Api.Schema.Queries;

public class Query
{
    private readonly CoursesRepository _coursesRepository;

    public Query(CoursesRepository coursesRepository)
    {
        _coursesRepository = coursesRepository;
    }

    public async Task<IEnumerable<CourseType>> GetCourses()
    {
        return (await _coursesRepository.GetAll()).Select(MapCourseTypeFromCourseModel);
    }

    private static CourseType MapCourseTypeFromCourseModel(Course c)
    {
        return new CourseType
        {
            Id = c.Id,
            Name = c.Name,
            Price = c.Price,
            Teacher = new TeacherType
            {
                Id = c.Teacher.Id,
                FirstName = c.Teacher.FirstName,
                LastName = c.Teacher.LastName,
                Salary = c.Teacher.Salary
            },
            Students = c.Students.Select(s => new StudentType
            {
                Id = s.Id,
                FirstName = s.FirstName,
                LastName = s.LastName,
                GPA = s.GPA
            })
        };
    }


    public async Task<CourseType> GetCourseByIdAsync(int id)
    {
        var course = await _coursesRepository.GetById(id);
        return MapCourseTypeFromCourseModel(course);
    }
}