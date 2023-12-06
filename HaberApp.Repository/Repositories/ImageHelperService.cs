using HaberApp.Core.Models;
using HaberApp.Core.Repositories;
using System.Text.Json;

namespace HaberApp.Repository.Repositories
{
    public class ImageHelperService : IImageHelperService
    {
        private readonly CloudFlareSettings cloudFlareSettings;
        public ImageHelperService(CloudFlareSettings cloudFlareSettings)
        {
            this.cloudFlareSettings = cloudFlareSettings;
        }
        public async Task<ImageResponseModel> ImageResult(string imagePath)
        {
            var client = new HttpClient();

            MultipartFormDataContent form = new MultipartFormDataContent();
            form.Add(new StringContent(imagePath), "url");

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"https://api.cloudflare.com/client/v4/accounts/{this.cloudFlareSettings.AccountId}/images/v1"),
                Headers =
                     {
                        { "Authorization", $"Bearer {this.cloudFlareSettings.TokenKey}" },
                     },
                Content = form,

            };

            using (var response = await client.SendAsync(request))
            {

                return JsonSerializer.Deserialize<ImageResponseModel>(await response.Content.ReadAsStringAsync());

            }
        }

        public List<string>? RestoreVariants(ImageResponseModel model)
        {
            if (!model.success)
            {
                return null;
            }

            var firstEl = model.result.variants[0];
            var basePath = firstEl.Substring(0, firstEl.LastIndexOf("/"));

            return new List<string>()
            {
                GetByType(basePath,"Small"),
                GetByType(basePath,"Medium"),
                GetByType(basePath,"Big"),
            };
        }
        private string GetByType(string basePath, string type)
        {
            return $"{basePath}/{type}";
        }

    }
}

