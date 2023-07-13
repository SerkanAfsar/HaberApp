using HaberApp.Core.UnitOfWork;

namespace HaberApp.Repository.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public UnitOfWork(AppDbContext _context)
        {
            this._context = _context;

        }
        public void Commit()
        {
            this._context.SaveChanges();
        }

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            await this._context.SaveChangesAsync(cancellationToken);
        }
    }
}
