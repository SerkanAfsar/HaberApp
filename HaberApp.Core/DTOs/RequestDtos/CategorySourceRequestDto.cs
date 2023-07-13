namespace HaberApp.Core.DTOs.RequestDtos
{
    public class CategorySourceRequestDto
    {
        public int SourceType { get; set; }
        public string SourceUrl { get; set; }
        public int CategoryId { get; set; }
    }
}
