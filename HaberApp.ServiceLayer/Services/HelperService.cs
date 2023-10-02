using HaberApp.Core.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace HaberApp.ServiceLayer.Services
{
    public class HelperService : IHelperService
    {
        private readonly IWebHostEnvironment hostEnvironment;
        public HelperService(IWebHostEnvironment hostEnvironment)
        {
            this.hostEnvironment = hostEnvironment;
        }
        public async Task<string?> SaveImageToDb(string newsTitle, IFormFile file)
        {
            string uploads = Path.Combine(hostEnvironment.WebRootPath, "images");
            if (file.Length > 0)
            {
                var extension = Path.GetExtension(file.FileName);
                var fileName = $"{newsTitle}{extension}";
                string filePath = Path.Combine(uploads, fileName);
                using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                return fileName;

            }
            return null;
        }
    }
}
