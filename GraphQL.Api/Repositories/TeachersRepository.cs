using GraphQL.Api.Models;
using GraphQL.Api.Services;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.Api.Repositories;

public class TeachersRepository
{
    private readonly ApplicationDbContext _dbContext;

    public TeachersRepository(IDbContextFactory<ApplicationDbContext> dbContextFactory)
    {
        _dbContext = dbContextFactory.CreateDbContext();
    }

    public async Task<IEnumerable<Teacher>> GetByManyIds(IReadOnlyCollection<int> ids)
    {
        return await _dbContext.Teachers.Where(t => ids.Contains(t.Id)).ToListAsync();
    }

}