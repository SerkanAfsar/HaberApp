using HaberApp.Core.Models;

namespace HaberApp.Core.Repositories
{
    public interface IImageHelperService
    {
        Task<ImageResponseModel> ImageResult(string imagePath);
        List<string>? RestoreVariants(ImageResponseModel model);
        Task<ImageResponseModel> RemoveImageFromCdnAsync(string imageIdentifier);
    }
}
