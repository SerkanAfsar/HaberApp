using HaberApp.Core.Models.Abstract;

namespace HaberApp.Core.Models
{
    public class Category : BaseEntity, ICreationStatus, IQueueStatus
    {
        public DateTime? CreateDate { get; set; } = DateTime.Now;
        public DateTime? UpdatedDate { get; set; }
        public string CategoryName { get; set; }
        public string SeoTitle { get; set; }
        public string SeoDesctiption { get; set; }
        public int Queue { get; set; } = 1;
        public string SeoUrl { get; set; }
        public List<CategorySource> CategorySources { get; set; } = new List<CategorySource>();
        public List<News> CategoryNews { get; set; } = new List<News>();
    }
}
