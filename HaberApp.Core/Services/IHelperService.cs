using Microsoft.AspNetCore.Http;

namespace HaberApp.Core.Services
{
    public interface IHelperService
    {
        Task<string?> SaveImageToDb(string newsTitle, IFormFile file);
    }
}
