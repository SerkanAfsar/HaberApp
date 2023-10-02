using HaberApp.Core.Models;
using System.Linq.Expressions;

namespace HaberApp.Core.Repositories
{
    public interface INewsRepository : IRepositoryBase<News>
    {
        Task<bool> HasArticle(string title, CancellationToken cancellationToken = default);
        Task<List<News>> GetNewsIncludeCategoryByPaginationAsync(int pageIndex, int limit, int? categoryId, Expression<Func<News, bool>> predicate = null, CancellationToken cancellationToken = default);
    }
}
