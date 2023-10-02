using HaberApp.Core.DTOs.RequestDtos;
using HaberApp.Core.DTOs.ResponseDtos;
using HaberApp.Core.Models;
using HaberApp.Core.Utils;

namespace HaberApp.Core.Services
{
    public interface INewsService : IServiceBase<News, NewsRequestDto, NewsResponseDto>
    {
        Task<ResponseResult<NewsResponseDto>> CreateNewsByFormAsync(NewsRequestDto model, CancellationToken cancellationToken = default);
        Task<ResponseResult<NewsResponseDto>> GetNewsByPaginationIncludeCategoryAsync(int pageIndex, int limitSize, int? categoryId = null, CancellationToken cancellationToken = default);

    }
}
