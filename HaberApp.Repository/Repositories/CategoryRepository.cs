using HaberApp.Core.Models;
using HaberApp.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HaberApp.Repository.Repositories
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        private readonly DbSet<Category> dbset;
        public CategoryRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            this.dbset = appDbContext.Categories;
        }

        public async Task<bool> DoesExistCategory(string categoryName, CancellationToken cancellationToken = default)
        {
            return await this.dbset.AnyAsync(a => a.CategoryName == categoryName, cancellationToken);
        }

        public async Task UpdateToTopQueueAsync(int categoryId, int queue, CancellationToken cancellationToken = default)
        {
            var entity = await this.dbset.FirstOrDefaultAsync(a => a.Id == categoryId && a.Queue == queue, cancellationToken);
            if (entity != null)
            {
                var afterEntity = await this.dbset.Where(a => a.Queue < queue).OrderByDescending(a => a.Queue).FirstOrDefaultAsync(cancellationToken);
                if (afterEntity != null)
                {
                    entity.Queue = afterEntity.Queue;
                    afterEntity.Queue = queue;
                }

            }

        }

        public async Task UpdateToDownQueueAsync(int categoryId, int queue, CancellationToken cancellationToken = default)
        {
            var entity = await this.dbset.FirstOrDefaultAsync(a => a.Id == categoryId && a.Queue == queue, cancellationToken);
            if (entity != null)
            {
                var afterEntity = await this.dbset.Where(a => a.Queue > queue).OrderBy(a => a.Queue).FirstOrDefaultAsync(cancellationToken);
                if (afterEntity != null)
                {
                    entity.Queue = afterEntity.Queue;
                    afterEntity.Queue = queue;
                }

            }
        }

        public async Task<List<Category>> GetAllCategoryListOrderByQueueAsync(CancellationToken cancellationToken = default)
        {
            return await this.dbset.OrderBy(a => a.Queue).ToListAsync(cancellationToken);
        }

        public async Task<List<Category>> GetAllCategoryListPaginationOrderByQueueAsync(int startIndex = 1, int count = 10, CancellationToken cancellationToken = default)
        {
            return await this.dbset.OrderBy(a => a.Queue).Skip((startIndex - 1) * count).Take(count).ToListAsync(cancellationToken);
        }
    }
}
