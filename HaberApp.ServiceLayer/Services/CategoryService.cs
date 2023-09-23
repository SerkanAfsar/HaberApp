using AutoMapper;
using HaberApp.Core.DTOs.RequestDtos;
using HaberApp.Core.DTOs.ResponseDtos;
using HaberApp.Core.Models;
using HaberApp.Core.Repositories;
using HaberApp.Core.Services;
using HaberApp.Core.UnitOfWork;
using HaberApp.Core.Utils;
using HaberApp.ServiceLayer.Caching;

namespace HaberApp.ServiceLayer.Services
{
    public class CategoryService : ServiceBase<Category, CategoryRequestDto, CategoryResponseDto>, ICategoryService
    {

        private readonly ICacheProcess<CategoryResponseDto> _cacheProcess;
        private readonly IMapper mapper;
        private readonly ICategoryRepository _repository;
        private readonly ResponseResult<CategoryResponseDto> responseResult;
        private readonly IUnitOfWork unitOfWork;
        public CategoryService(IRepositoryBase<Category> repositoryBase, ICacheProcess<CategoryResponseDto> cacheProcess, IUnitOfWork unitOfWork, IMapper mapper, ICategoryRepository _repository) : base(repositoryBase, cacheProcess, unitOfWork, mapper)
        {
            this._repository = _repository;
            this._cacheProcess = cacheProcess;
            this.mapper = mapper;
            this.responseResult = new ResponseResult<CategoryResponseDto>();
            this.unitOfWork = unitOfWork;
        }

        public async Task<ResponseResult<CategoryResponseDto>> GetCategoryListByPaginationAsync(CancellationToken cancellationToken = default)
        {
            if (!this._cacheProcess.DoesExist(CacheConstants.CategoryList))
            {
                await SetCategoryMenuListByQueueCacheAsync(cancellationToken);
            }
            this.responseResult.Entities = this._cacheProcess.GetCachedDtos(CacheConstants.CategoryList);
            return this.responseResult;
        }

        public async Task<ResponseResult<CategoryResponseDto>> GetCategoryListByPaginationAsync(int pageIndex = 1, int takeCount = 10, CancellationToken cancellationToken = default)
        {
            if (!this._cacheProcess.DoesExist(CacheConstants.CategoryList))
            {
                await SetCategoryMenuListByQueueCacheAsync(cancellationToken);
            }
            var list = this._cacheProcess.GetCachedDtos(CacheConstants.CategoryList);
            this.responseResult.Entities = list.Skip((pageIndex - 1) * takeCount).Take(takeCount).ToList();
            this.responseResult.TotalCount = list.Count();
            return this.responseResult;
        }



        public Task<ResponseResult<CategoryResponseDto>> GetCategoryListOrderByQueueAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task SetCategoryMenuListByQueueCacheAsync(CancellationToken cancellationToken = default)
        {
            await Task.Run(async () =>
            {
                this._cacheProcess.SetCachedDtos(CacheConstants.CategoryList, this.mapper.Map<List<CategoryResponseDto>>(await this._repository.GetCategoryListOrderByQueueAsync(cancellationToken)));
            });
        }

        public async Task<ResponseResult<CategoryResponseDto>> TestCategoryResponse(CancellationToken cancellationToken = default)
        {
            var category = new Category()
            {
                CategoryName = "deneme 1",
                SeoTitle = "deneme 11 seo",
                SeoUrl = "111",
                SeoDesctiption = "11",
                CategoryNews = new List<News>()
                {
                    new News(){
                        SeoTitle="111",
                        SeoDesctiption="11",
                        NewsSubTitle="11",
                        NewsContent="11",
                        NewsPicture="11",
                        NewsSource= Core.Models.Enums.NewsSource.AdaletBiz,
                        SourceUrl="11" ,NewsTitle="11"
                    },
                }

            };
            var entity = await this._repository.CreateAsync(category, cancellationToken);
            await this.unitOfWork.CommitAsync();
            this.responseResult.Entity = this.mapper.Map<CategoryResponseDto>(entity);
            return this.responseResult;
        }
    }
}
