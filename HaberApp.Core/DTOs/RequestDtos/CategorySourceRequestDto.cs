namespace HaberApp.Core.DTOs.RequestDtos
{
    public class CategorySourceRequestDto : BaseDto
    {
        public int SourceType { get; set; }
        public string SourceUrl { get; set; }
        public int CategoryId { get; set; }
    }
}
