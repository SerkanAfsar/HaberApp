using HaberApp.Core.DTOs.RequestDtos;
using HaberApp.Core.DTOs.ResponseDtos;
using HaberApp.Core.Models;
using HaberApp.Core.Utils;

namespace HaberApp.Core.Services
{
    public interface ICategorySourceService : IServiceBase<CategorySource, CategorySourceRequestDto, CategorySourceResponseDto>
    {
        Task<ResponseResult<CategorySourceResponseDto>> GetCategorySourcesByPagination(int pageIndex, int limitSize, int? categoryId = null, CancellationToken cancellationToken = default);
    }
}
