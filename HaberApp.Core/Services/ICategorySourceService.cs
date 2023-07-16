using HaberApp.Core.DTOs.RequestDtos;
using HaberApp.Core.DTOs.ResponseDtos;
using HaberApp.Core.Models;

namespace HaberApp.Core.Services
{
    public interface ICategorySourceService : IServiceBase<CategorySource, CategorySourceRequestDto, CategorySourceResponseDto>
    {
    }
}
