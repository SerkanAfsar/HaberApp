using AutoMapper;
using HaberApp.Core.DTOs.RequestDtos;
using HaberApp.Core.DTOs.ResponseDtos;
using HaberApp.Core.Models;
using HaberApp.Core.Repositories;
using HaberApp.Core.Services;
using HaberApp.Core.UnitOfWork;
using HaberApp.Core.Utils;
using HaberApp.ServiceLayer.Exceptions;

namespace HaberApp.ServiceLayer.Services
{
    public class CategoryService : ServiceBase<Category, CategoryRequestDto, CategoryResponseDto>, ICategoryService
    {


        private readonly IMapper mapper;
        private readonly ICategoryRepository _repository;
        private readonly ResponseResult<CategoryResponseDto> responseResult;
        private readonly IUnitOfWork unitOfWork;
        private readonly INewsRepository newsRepository;
        private readonly IImageHelperService ımageHelperService;
        public CategoryService(IRepositoryBase<Category> repositoryBase, IUnitOfWork unitOfWork, IMapper mapper, ICategoryRepository _repository, INewsRepository newsRepository, IImageHelperService ımageHelperService) : base(repositoryBase, unitOfWork, mapper)
        {
            this._repository = _repository;

            this.mapper = mapper;
            this.responseResult = new ResponseResult<CategoryResponseDto>();
            this.unitOfWork = unitOfWork;
            this.newsRepository = newsRepository;
            this.ımageHelperService = ımageHelperService;
        }

        public async Task<ResponseResult<CategoryResponseDto>> DownCategoryAsync(CategoryResponseDto category, CancellationToken cancellationToken = default)
        {
            if (category == null)
            {
                throw new NotFoundException($"{typeof(Category).Name} Not Found");
            }

            await this._repository.UpdateToDownQueueAsync(category.Id, category.Queue, cancellationToken);
            await this.unitOfWork.CommitAsync(cancellationToken);
            this.responseResult.Entity = this.mapper.Map<CategoryResponseDto>(category);
            return this.responseResult;
        }

        public async Task<ResponseResult<CategoryResponseDto>> GetCategoryListOrderByPaginationAsync(int startIndex = 1, int count = 10, CancellationToken cancellationToken = default)
        {
            this.responseResult.Entities = this.mapper.Map<List<CategoryResponseDto>>(await this._repository.GetAllCategoryListPaginationOrderByQueueAsync(startIndex, count, cancellationToken));
            this.responseResult.TotalCount = await this._repository.EntitiesCount(null, cancellationToken);
            return this.responseResult;
        }

        public async Task<ResponseResult<CategoryResponseDto>> GetCategoryListOrderByQueueAsync(CancellationToken cancellationToken = default)
        {
            this.responseResult.Entities = this.mapper.Map<List<CategoryResponseDto>>(await this._repository.GetAllCategoryListOrderByQueueAsync(cancellationToken));
            this.responseResult.TotalCount = await this._repository.EntitiesCount(null, cancellationToken);
            return this.responseResult;
        }

        public async Task<ResponseResult<CategoryResponseDto>> UpCategoryAsync(CategoryResponseDto category, CancellationToken cancellationToken = default)
        {
            if (category == null)
            {
                throw new NotFoundException($"{typeof(Category).Name} Not Found");
            }

            await this._repository.UpdateToTopQueueAsync(category.Id, category.Queue, cancellationToken);
            await this.unitOfWork.CommitAsync(cancellationToken);
            this.responseResult.Entity = this.mapper.Map<CategoryResponseDto>(category);
            return this.responseResult;
        }
        public async override Task<ResponseResult<CategoryResponseDto>> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var news = await this.newsRepository.GetListAsync(a => a.CategoryId == id);
            foreach (var item in news)
            {
                var bigOne = item.NewsPictureBig;
                if (!string.IsNullOrEmpty(bigOne))
                {
                    var arr = bigOne.Split("/");
                    var lastOne = arr[arr.Length - 2];
                    var deneme = await this.ımageHelperService.RemoveImageFromCdnAsync(lastOne);
                    var deneme2 = deneme;

                }

            }
            return await base.DeleteAsync(id, cancellationToken);
        }
    }
}
