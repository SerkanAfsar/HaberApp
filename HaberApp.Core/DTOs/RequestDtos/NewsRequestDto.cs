using Microsoft.AspNetCore.Http;

namespace HaberApp.Core.DTOs.RequestDtos
{
    public class NewsRequestDto : BaseRequestDto
    {
        public string NewsTitle { get; set; }
        public string NewsSubTitle { get; set; }
        public string NewsContent { get; set; }
        public string SeoTitle { get; set; }
        public string SeoDesctiption { get; set; }
        public string NewsSource { get; set; }
        public IFormFile NewsPicture { get; set; }
        public int SourceUrl { get; set; }
        public int CategoryId { get; set; }

    }
}
