using HaberApp.Core.Models;
using System.Linq.Expressions;

namespace HaberApp.Core.Repositories
{
    public interface ICategorySourceRepository : IRepositoryBase<CategorySource>
    {
        Task<List<CategorySource>> GetCategorySourcesIncludeCategoriesByPaginationAsync(int pageIndex, int limit, int? categoryId, Expression<Func<CategorySource, bool>> predicate = null, CancellationToken cancellationToken = default);
    }

}
