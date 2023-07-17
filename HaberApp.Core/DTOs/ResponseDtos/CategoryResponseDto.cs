namespace HaberApp.Core.DTOs.ResponseDtos
{
    public class CategoryResponseDto : BaseResponseDto
    {
        public string CategoryName { get; set; }
        public string SeoTitle { get; set; }
        public string SeoDesctiption { get; set; }
    }
}
