namespace HaberApp.Core.DTOs.RequestDtos
{
    public class CategoryRequestDto : BaseRequestDto
    {
        public string CategoryName { get; set; }
        public string SeoTitle { get; set; }
        public string SeoDesctiption { get; set; }
    }
}
