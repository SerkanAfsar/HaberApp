namespace HaberApp.Core.DTOs.ResponseDtos
{
    public class NewsResponseDto : BaseResponseDto
    {
        public string NewsTitle { get; set; }
        public string NewsSubTitle { get; set; }
        public string NewsContent { get; set; }
        public string SeoTitle { get; set; }
        public string SeoDesctiption { get; set; }
        public int NewsSource { get; set; }
        public string NewsPicture { get; set; }
        public int SourceUrl { get; set; }
        public int CategoryId { get; set; }
        public CategoryResponseDto Category { get; set; }
    }
}
