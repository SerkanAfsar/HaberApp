using HaberApp.Core.Models.Abstract;
using HaberApp.Core.Models.Enums;

namespace HaberApp.Core.Models
{
    public class CategorySource : BaseEntity, ICreationStatus
    {
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public NewsSource SourceType { get; set; }
        public string SourceUrl { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
