using GraphQL.Api.Models;
using GraphQL.Api.Repositories;

namespace GraphQL.Api.DataLoaders;

public class TeacherDataLoader : BatchDataLoader<int, Teacher>
{
    private readonly TeachersRepository _teachersRepository;

    public TeacherDataLoader(IBatchScheduler batchScheduler, TeachersRepository teachersRepository,
        DataLoaderOptions? options = null) : base(batchScheduler, options)
    {
        _teachersRepository = teachersRepository;
    }

    protected override async Task<IReadOnlyDictionary<int, Teacher>> LoadBatchAsync(IReadOnlyList<int> keys,
        CancellationToken cancellationToken)
    {
        var teachers = await _teachersRepository.GetByManyIds(keys);
        return teachers.ToDictionary(t => t.Id);
    }
}