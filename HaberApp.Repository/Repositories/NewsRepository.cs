using HaberApp.Core.Models;
using HaberApp.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HaberApp.Repository.Repositories
{
    public class NewsRepository : RepositoryBase<News>, INewsRepository
    {
        public NewsRepository(AppDbContext appDbContext) : base(appDbContext)
        {

        }

        public async Task<List<News>> GetNewsIncludeCategoryByPaginationAsync(int pageIndex, int limit, int? categoryId, Expression<Func<News, bool>> predicate = null, CancellationToken cancellationToken = default)
        {
            var query = this.appDbContext.News.AsQueryable();
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

        public async Task<bool> HasArticle(string title, CancellationToken cancellationToken = default)
        {
            var result = await this.GetByFilterAsync(a => a.NewsTitle.ToLower().Trim() == title, cancellationToken);
            return result != null;
        }
    }
}
