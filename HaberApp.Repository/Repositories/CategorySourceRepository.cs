using HaberApp.Core.Models;
using HaberApp.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HaberApp.Repository.Repositories
{
    public class CategorySourceRepository : RepositoryBase<CategorySource>, ICategorySourceRepository
    {
        private readonly AppDbContext appDbContext;
        public CategorySourceRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<List<CategorySource>> GetCategorySourcesIncludeCategoriesByPaginationAsync(int pageIndex, int limit, int? categoryId, Expression<Func<CategorySource, bool>> predicate = null, CancellationToken cancellationToken = default)
        {
            var query = this.appDbContext.categorySources.AsQueryable();
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            if (categoryId != null)
            {
                query = query.Where(a => a.CategoryId == categoryId);
            }
            return await query.Include(a => a.Category).Skip((pageIndex - 1) * limit).Take(limit).ToListAsync(cancellationToken);
        }
    }
}
