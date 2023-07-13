using HaberApp.Core.DTOs;
using HaberApp.Core.Models.Abstract;
using HaberApp.Core.Utils;
using System.Linq.Expressions;

namespace HaberApp.Core.Services
{
    public interface IServiceBase<D, T> where T : BaseDto where D : BaseEntity
    {

        Task<ResponseResult<T>> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<ResponseResult<T>> GetListAsync(Expression<Func<D, bool>> predicate = null, CancellationToken cancellationToken = default);
        Task<ResponseResult<T>> AddAsync(D entity, CancellationToken cancellationToken = default);
        Task<ResponseResult<T>> UpdateAsync(D entity, CancellationToken cancellationToken = default);
        Task<ResponseResult<T>> DeleteAsync(D entity, CancellationToken cancellationToken = default);
        Task<ResponseResult<T>> AddRangeAsync(IEnumerable<D> entities, CancellationToken cancellationToken = default);
        Task<ResponseResult<T>> RemoveRangeAsync(IEnumerable<D> entities, CancellationToken cancellationToken = default);
    }
}
