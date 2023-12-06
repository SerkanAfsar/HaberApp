using HaberApp.Core.Models;
using HaberApp.Core.Models.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace HaberApp.Repository
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        private readonly IConfiguration configuration;
        private readonly IHttpContextAccessor httpContextAccessor;
        public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            this.configuration = configuration;
            this.httpContextAccessor = httpContextAccessor;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(this.configuration.GetConnectionString("HaberConnection"), option =>
            {
                option.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName);
                option.EnableRetryOnFailure(3);
            });
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<ICreationStatus>().ToList())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreateDate = DateTime.UtcNow;
                        //entry.Entity. = httpContextAccessor.HttpContext.User.Identity.Name;
                        if (entry.Entity is Category)
                        {
                            var dbSet = entry.Context.Set<Category>();
                            var castedEntity = (Category)entry.Entity;
                            var lastQueue = await dbSet.OrderByDescending(a => a.Queue).Take(1).FirstOrDefaultAsync(cancellationToken);
                            castedEntity.Queue = lastQueue != null ? lastQueue.Queue + 1 : 1;
                        }
                        break;

                    case EntityState.Modified:
                        entry.Entity.UpdatedDate = DateTime.UtcNow;
                        //entry.Entity.LastModifiedBy = httpContextAccessor.HttpContext.User.Identity.Name;
                        break;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }





        public DbSet<News> News { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategorySource> categorySources { get; set; }
    }
}
