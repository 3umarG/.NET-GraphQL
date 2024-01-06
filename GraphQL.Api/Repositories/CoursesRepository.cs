using GraphQL.Api.Models;
using GraphQL.Api.Schema.Mutations;
using GraphQL.Api.Services;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.Api.Repositories;

public class CoursesRepository
{
    private readonly ApplicationDbContext _dbContext;

    public CoursesRepository(IDbContextFactory<ApplicationDbContext> dbContextFactory)
    {
        _dbContext = dbContextFactory.CreateDbContext();
    }


    public async Task<IEnumerable<Course>> GetAll()
    {
        var courses = await _dbContext.Courses
            .Include(c => c.Teacher)
            .Include(c => c.Students)
            .ToListAsync();

        return courses;
    }

    public async Task<Course> GetById(int id)
    {
        var course = await FindCourseOrThrowNotFound(id);
        return course;
    }

    public async Task<Course> Create(CourseInputType course)
    {
        var teacher = await _dbContext.Teachers.FindAsync(course.TeacherId);
        if (teacher is null)
        {
            throw new Exception($"Not Found Teacher with ID : {course.TeacherId}");
        }

        var newCourse = new Course
        {
            Name = course.Name,
            Price = course.Price,
            TeacherId = course.TeacherId,
            Teacher = teacher
        };
        await _dbContext.Courses.AddAsync(newCourse);
        await _dbContext.SaveChangesAsync();

        return newCourse;
    }

    public async Task<Course> Update(int id, CourseInputType courseInput)
    {
        var course = await FindCourseOrThrowNotFound(id);

        course.Name = courseInput.Name;
        course.Price = courseInput.Price;
        course.Teacher.Id = courseInput.TeacherId;

        _dbContext.Update(course);
        await _dbContext.SaveChangesAsync();

        return course;
    }

    private async Task<Course> FindCourseOrThrowNotFound(int id)
    {
        var course = await _dbContext.Courses
            .Include(c => c.Teacher)
            .Include(c => c.Students)
            .AsSplitQuery()
            .FirstOrDefaultAsync(c => c.Id == id);
        
        if (course is null)
        {
            throw new Exception($"Not Found Course with ID :{id}");
        }

        return course;
    }


    public async Task<Course> DeleteById(int id)
    {
        var course = await FindCourseOrThrowNotFound(id);

        _dbContext.Remove(course);
        await _dbContext.SaveChangesAsync();

        return course;
    }
}