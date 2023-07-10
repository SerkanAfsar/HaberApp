namespace HaberApp.Core.Models.Abstract
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public DateTime? CreationDate { get; set; } = DateTime.Now;
        public DateTime? UpdatedDate { get; set; }
    }
}
