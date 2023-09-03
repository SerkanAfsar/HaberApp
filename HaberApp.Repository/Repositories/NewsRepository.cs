using HaberApp.Core.Models;
using HaberApp.Core.Repositories;

namespace HaberApp.Repository.Repositories
{
    public class NewsRepository : RepositoryBase<News>, INewsRepository
    {
        public NewsRepository(AppDbContext appDbContext) : base(appDbContext)
        {

        }
        public async Task<bool> HasArticle(string title, CancellationToken cancellationToken = default)
        {
            var result = await this.GetByFilterAsync(a => a.NewsTitle.ToLower().Trim() == title, cancellationToken);
            return result != null;
        }
    }
}
