using HaberApp.Core.Models.Abstract;
using HaberApp.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HaberApp.Repository.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : BaseEntity
    {
        protected readonly AppDbContext appDbContext;
        private readonly DbSet<T> dbSet;
        public RepositoryBase(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
            this.dbSet = appDbContext.Set<T>();
        }
        public async Task<IQueryable<T>> AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken)
        {
            await this.dbSet.AddRangeAsync(entities, cancellationToken);
            return entities.AsQueryable();
        }

        public async Task<T> CreateAsync(T entity, CancellationToken cancellationToken)
        {
            await this.dbSet.AddAsync(entity, cancellationToken);
            return entity;
        }

        public async Task<T> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var entity = await this.dbSet.FindAsync(id);

            if (entity != null)
            {
                await Task.Run(() => this.dbSet.Remove(entity), cancellationToken);
            }

            return entity ?? null;
        }

        public async Task<T?> GetByFilterAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await this.dbSet.Where(predicate).FirstOrDefaultAsync(cancellationToken) ?? null;
        }

        public async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await this.dbSet.FindAsync(id, cancellationToken) ?? null;
        }

        public async Task<IQueryable<T>> GetListAsync(Expression<Func<T, bool>> predicate = null)
        {
            return predicate == null ? await Task.Run(() => this.dbSet.AsNoTracking().AsQueryable()) :
              await Task.Run(() => this.dbSet.AsNoTracking().Where(predicate).AsQueryable());
        }

        public async Task<IQueryable<T>> RemoveRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            await Task.Run(() => this.dbSet.RemoveRange(entities), cancellationToken);
            return entities.AsQueryable();
        }

        public async Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            await Task.Run(() => this.dbSet.Update(entity), cancellationToken);
            return entity;
        }
    }
}
