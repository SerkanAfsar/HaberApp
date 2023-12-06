using AutoMapper;
using HaberApp.Core.DTOs.RequestDtos;
using HaberApp.Core.DTOs.ResponseDtos;
using HaberApp.Core.Models;
using HaberApp.Core.Repositories;
using HaberApp.Core.Services;
using HaberApp.Core.UnitOfWork;
using HaberApp.Core.Utils;

namespace HaberApp.ServiceLayer.Services
{
    public class CategorySourceService : ServiceBase<CategorySource, CategorySourceRequestDto, CategorySourceResponseDto>, ICategorySourceService
    {
        private readonly IRepositoryBase<CategorySource> _repositoryBase;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly ResponseResult<CategorySourceResponseDto> _responseResult;
        private readonly ICategorySourceRepository categorySourceRepository;
        public CategorySourceService(IRepositoryBase<CategorySource> repositoryBase, IUnitOfWork unitOfWork, IMapper mapper, ICategorySourceRepository categorySourceRepository) : base(repositoryBase, unitOfWork, mapper)
        {
            this._repositoryBase = repositoryBase;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.categorySourceRepository = categorySourceRepository;
            this._responseResult = new ResponseResult<CategorySourceResponseDto>();
        }

        public async Task<ResponseResult<CategorySourceResponseDto>> GetCategorySourcesByPagination(int pageIndex, int limitSize, int? categoryId = null, CancellationToken cancellationToken = default)
        {
            this._responseResult.Entities = this.mapper.Map<List<CategorySourceResponseDto>>(await this.categorySourceRepository.GetCategorySourcesIncludeCategoriesByPaginationAsync(pageIndex, limitSize, categoryId, null, cancellationToken));

            this._responseResult.TotalCount = categoryId != null ? await this._repositoryBase.EntitiesCount(a => a.CategoryId == categoryId, cancellationToken) : await this._repositoryBase.EntitiesCount(null, cancellationToken);
            return this._responseResult;
        }
    }
}
