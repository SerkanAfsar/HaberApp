using HaberApp.Core.Models.Abstract;
using HaberApp.Core.Models.Enums;

namespace HaberApp.Core.Models
{
    public class News : BaseEntity, ICreationStatus
    {
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? SeoTitle { get; set; }
        public string? SeoDesctiption { get; set; }
        public string NewsTitle { get; set; }
        public string? NewsSubTitle { get; set; }
        public string? NewsContent { get; set; }
        public NewsSource NewsSource { get; set; }
        public string? NewsPictureSmall { get; set; }
        public string? NewsPictureMedium { get; set; }
        public string? NewsPictureBig { get; set; }
        public string? SourceUrl { get; set; }
        public int ReadCount { get; set; } = 0;
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public string? SeoUrl { get; set; }
    }
}
