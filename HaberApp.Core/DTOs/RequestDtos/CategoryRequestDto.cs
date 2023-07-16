namespace HaberApp.Core.DTOs.RequestDtos
{
    public class CategoryRequestDto : BaseDto
    {
        public string CategoryName { get; set; }
        public string SeoTitle { get; set; }
        public string SeoDesctiption { get; set; }
    }
}
