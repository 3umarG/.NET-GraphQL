using GraphQL.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.Api.Services;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Student>()
            .HasMany<Course>(s => s.Courses)
            .WithMany(c => c.Students);
    }

    public virtual DbSet<Student> Students { get; set; }
    public virtual DbSet<Course> Courses { get; set; }
    public virtual DbSet<Teacher> Teachers { get; set; }
}