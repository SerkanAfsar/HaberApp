using HaberApp.Core.Models;
using HaberApp.Core.Repositories;

namespace HaberApp.Repository.Repositories
{
    public class NewsRepository : RepositoryBase<News>, INewsRepository
    {
        public NewsRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}
