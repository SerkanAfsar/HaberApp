using HaberApp.Core.Models;

namespace HaberApp.Core.Repositories
{
    public interface ICategoryRepository : IRepositoryBase<Category>
    {
        Task<bool> DoesExistCategory(string categoryName, CancellationToken cancellationToken = default);
    }
}
