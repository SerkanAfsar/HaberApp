namespace HaberApp.Core.DTOs.ResponseDtos
{
    public class CategorySourceResponseDto : BaseResponseDto
    {
        public string SourceTypeName { get; set; }
        public string SourceUrl { get; set; }
        public int CategoryId { get; set; }
    }
}
