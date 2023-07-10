﻿using HaberApp.Core.Models.Abstract;
using System.Linq.Expressions;

namespace HaberApp.Core.Repositories
{
    public interface IRepositoryBase<T> where T : BaseEntity
    {
        Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        IQueryable<T> GetListAsync(Expression<Func<T, bool>> predicate = null);
        Task<T> CreateAsync(T entity, CancellationToken cancellationToken);
        Task<T> UpdateAsync(T entity, CancellationToken cancellationToken);
        Task<T> DeleteAsync(T entity, CancellationToken cancellationToken);
        Task<IQueryable<T>> AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken);
        Task<IQueryable<T>> RemoveRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken);
    }
}
