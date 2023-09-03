using HaberApp.Core.Models;

namespace HaberApp.Core.Repositories
{
    public interface INewsRepository : IRepositoryBase<News>
    {
        Task<bool> HasArticle(string title, CancellationToken cancellationToken = default);
    }
}
