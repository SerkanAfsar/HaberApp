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

        public async Task<List<Category>> GetCategoryListOrderByQueueAsync(CancellationToken cancellationToken = default)
        {
            return await this.dbset.OrderBy(a => a.Queue).ToListAsync(cancellationToken);
        }
    }
}
