namespace HaberApp.Core.Repositories.NewsSourceRepositories
{
    public interface ISourceRepository
    {
        Task GetArticleSourceListAsync(string categorySourceUrl, int CategoryID, CancellationToken cancellationToken = default);
        Task AddArticleToDbAsync(string articleSourceUrl, int CategoryID, CancellationToken cancellationToken = default);
    }
}
