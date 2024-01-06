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
            TeacherId = c.TeacherId
        };
    }


    public async Task<CourseType> GetCourseByIdAsync(int id)
    {
        var course = await _coursesRepository.GetById(id);
        return MapCourseTypeFromCourseModel(course);
    }
}