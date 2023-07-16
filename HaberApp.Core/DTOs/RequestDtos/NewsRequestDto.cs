namespace HaberApp.Core.DTOs.RequestDtos
{
    public class NewsRequestDto : BaseDto

    {
        public string SeoTitle { get; set; }
        public string SeoDesctiption { get; set; }
        public string NewsTitle { get; set; }
        public string NewsSubTitle { get; set; }
        public string NewsContent { get; set; }
        public int NewsSource { get; set; }
        public string NewsPicture { get; set; }
        public string SourceUrl { get; set; }
        public int CategoryId { get; set; }

    }
}
