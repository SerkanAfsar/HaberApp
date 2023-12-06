using HaberApp.Core.Repositories;
using HaberApp.Core.Repositories.NewsSourceRepositories;
using HaberApp.Core.Services;
using HaberApp.Core.UnitOfWork;

namespace HaberApp.ServiceLayer.Services
{
    public class SourceServices : ISourceService
    {
        private readonly IUnitOfWork unitOfWork;

        private readonly ICategorySourceRepository categorySourceRepository;
        private readonly IAdaletBizRepository adaletBizRepository;
        private readonly IAdaletMedyaRepository adaletMedyaRepository;
        public SourceServices(IUnitOfWork unitOfWork, ICategorySourceRepository categorySourceRepository, IAdaletBizRepository adaletBizRepository, IAdaletMedyaRepository adaletMedyaRepository)
        {
            this.unitOfWork = unitOfWork;
            this.categorySourceRepository = categorySourceRepository;
            this.adaletBizRepository = adaletBizRepository;
            this.adaletMedyaRepository = adaletMedyaRepository;
        }


        public async Task SaveAllToDb(CancellationToken cancellationToken = default)
        {
            var list = await categorySourceRepository.GetListAsync();
            foreach (var item in list)
            {
                switch (item.SourceType)
                {
                    case Core.Models.Enums.NewsSource.AdaletBiz:
                        {
                            this.adaletBizRepository.GetArticleSourceListAsync(item.SourceUrl, item.CategoryId, cancellationToken).Wait(cancellationToken);
                            break;
                        }
                    case Core.Models.Enums.NewsSource.AdaletMedya:
                        {
                            this.adaletMedyaRepository.GetArticleSourceListAsync(item.SourceUrl, item.CategoryId, cancellationToken).Wait(cancellationToken);
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }

            }
            await unitOfWork.CommitAsync(cancellationToken);
        }


    }
}
