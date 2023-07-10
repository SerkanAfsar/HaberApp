namespace HaberApp.Core.Models.Abstract
{
    public interface ICreationStatus
    {
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
