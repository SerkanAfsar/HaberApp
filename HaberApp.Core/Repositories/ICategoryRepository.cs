using HaberApp.Core.Models;

namespace HaberApp.Core.Repositories
{
    public interface ICategoryRepository : IRepositoryBase<Category>
    {
        Task<List<Category>> GetAllCategoryListOrderByQueueAsync(CancellationToken cancellationToken = default);
        Task<List<Category>> GetAllCategoryListPaginationOrderByQueueAsync(int startIndex = 1, int count = 10, CancellationToken cancellationToken = default);
        Task<bool> DoesExistCategory(string categoryName, CancellationToken cancellationToken = default);
        Task UpdateToTopQueueAsync(int categoryId, int queue, CancellationToken cancellationToken = default);
        Task UpdateToDownQueueAsync(int categoryId, int queue, CancellationToken cancellationToken = default);

    }
}
