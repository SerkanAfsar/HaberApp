using AutoMapper;
using HaberApp.Core.DTOs.ResponseDtos;
using HaberApp.Core.Models;
using HaberApp.Core.Repositories;
using HaberApp.Core.Services;
using HaberApp.Core.UnitOfWork;
using HaberApp.ServiceLayer.Caching;

namespace HaberApp.ServiceLayer.Services
{
    public class NewsService : ServiceBase<News, NewsResponseDto>, INewsService
    {
        public NewsService(IRepositoryBase<News> repositoryBase, ICacheProcess<NewsResponseDto> cacheProcess, IUnitOfWork unitOfWork, IMapper mapper) : base(repositoryBase, cacheProcess, unitOfWork, mapper)
        {
        }
    }
}
