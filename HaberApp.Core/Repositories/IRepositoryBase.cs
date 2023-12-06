using HaberApp.Core.Models.Abstract;
using System.Linq.Expressions;

namespace HaberApp.Core.Repositories
{
    public interface IRepositoryBase<T> where T : BaseEntity
    {
        Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<T?> GetByFilterAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
        Task<IQueryable<T>> GetListAsync(Expression<Func<T, bool>> predicate = null);
        Task<T> CreateAsync(T entity, CancellationToken cancellationToken);
        Task<T?> UpdateAsync(int id, T Entity, CancellationToken cancellationToken);
        Task<T> DeleteAsync(int id, CancellationToken cancellationToken);
        Task<IQueryable<T>> AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken);
        Task<IQueryable<T>> RemoveRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken);
        Task<int> EntitiesCount(Expression<Func<T, bool>> predicate = null, CancellationToken cancellationToken = default);
        Task<List<T>> GetEntitiesByPaginationAsync(int pageIndex, int limitCount, Expression<Func<T, bool>> predicate = null, CancellationToken cancellationToken = default);

    }
}
