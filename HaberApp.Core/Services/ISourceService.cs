namespace HaberApp.Core.Services
{
    public interface ISourceService
    {
        Task SaveAllToDb(CancellationToken cancellationToken = default);
    }
}
