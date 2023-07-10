using HaberApp.Core.Models;
using HaberApp.Core.Repositories;

namespace HaberApp.Repository.Repositories
{
    public class CategorySourceRepository : RepositoryBase<CategorySource>, ICategorySourceRepository
    {
        public CategorySourceRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}
