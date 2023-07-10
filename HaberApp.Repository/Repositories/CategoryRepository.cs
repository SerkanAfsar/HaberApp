using HaberApp.Core.Models;
using HaberApp.Core.Repositories;

namespace HaberApp.Repository.Repositories
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}
