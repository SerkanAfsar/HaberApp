using AutoMapper;
using HaberApp.Core.DTOs.RequestDtos;
using HaberApp.Core.DTOs.ResponseDtos;
using HaberApp.Core.Models;
using HaberApp.Core.Models.Enums;
using HaberApp.Core.Repositories;
using HaberApp.Core.Services;
using HaberApp.Core.UnitOfWork;
using HaberApp.Core.Utils;
using Microsoft.EntityFrameworkCore;

namespace HaberApp.ServiceLayer.Services
{
    public class NewsService : ServiceBase<News, NewsRequestDto, NewsResponseDto>, INewsService
    {
        private readonly IRepositoryBase<News> _newsRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly ResponseResult<NewsResponseDto> responseResult;
        private readonly IHelperService helperService;
        private readonly INewsRepository newsRepository;
        private readonly ICategorySourceRepository categorySourceRepository;
        public NewsService(IRepositoryBase<News> repositoryBase, IUnitOfWork unitOfWork, IMapper mapper, IHelperService helperService, INewsRepository newsRepository, ICategorySourceRepository categorySourceRepository) : base(repositoryBase, unitOfWork, mapper)
        {
            this._newsRepository = repositoryBase;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.responseResult = new ResponseResult<NewsResponseDto>();
            this.helperService = helperService;
            this.newsRepository = newsRepository;
            this.categorySourceRepository = categorySourceRepository;
        }

        public async Task<ResponseResult<NewsResponseDto>> AddAllNewsToDbAsync(CancellationToken cancellationToken = default)
        {
            var categorySources = await this.categorySourceRepository.GetListAsync(null);
            var list = await categorySources.ToListAsync(cancellationToken);

            foreach (var categorySource in list)
            {

            }
            return this.responseResult;
        }

        public async Task<ResponseResult<NewsResponseDto>> CreateNewsByFormAsync(NewsRequestDto model, CancellationToken cancellationToken = default)
        {
            var friendlyUrl = StringHelper.FriendlySeoUrl(model.NewsTitle);
            var newsResponseEntity = await this._newsRepository.CreateAsync(new News()
            {
                CategoryId = model.CategoryId,
                CreateDate = DateTime.Now,
                CreationDate = DateTime.Now,
                NewsContent = model.NewsContent,
                //NewsPicture = await this.helperService.SaveImageToDb(friendlyUrl, model.NewsPicture),
                ReadCount = 1,
                SeoTitle = model.SeoTitle,
                SeoDesctiption = model.SeoDesctiption,
                SourceUrl = model.SourceUrl,
                NewsSource = (NewsSource)model.NewsSource,
                NewsTitle = model.NewsTitle,
                NewsSubTitle = model.NewsSubTitle,
                SeoUrl = friendlyUrl
            }, cancellationToken);
            await this.unitOfWork.CommitAsync(cancellationToken);
            this.responseResult.Entity = this.mapper.Map<NewsResponseDto>(newsResponseEntity);
            return this.responseResult;

        }

        public async Task<ResponseResult<NewsResponseDto>> GetNewsByPaginationIncludeCategoryAsync(int pageIndex, int limitSize, int? categoryId = null, CancellationToken cancellationToken = default)
        {
            this.responseResult.Entities = this.mapper.Map<List<NewsResponseDto>>(await this.newsRepository.GetNewsIncludeCategoryByPaginationAsync(pageIndex, limitSize, categoryId, null, cancellationToken));
            this.responseResult.TotalCount = categoryId != null ? await this.newsRepository.EntitiesCount(a => a.CategoryId == categoryId, cancellationToken) : await this.newsRepository.EntitiesCount(null, cancellationToken);
            return this.responseResult;
        }
    }
}
