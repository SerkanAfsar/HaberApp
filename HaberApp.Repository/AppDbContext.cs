using HaberApp.Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace HaberApp.Repository
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        private readonly IConfiguration configuration;
        public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(this.configuration.GetConnectionString("HaberConnection"), option =>
            {
                option.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName);
                option.EnableRetryOnFailure(3);
            });
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            //foreach (var entry in ChangeTracker.Entries<IAuditable>().ToList())
            //{
            //    switch (entry.State)
            //    {
            //        case EntityState.Added:
            //            entry.Entity.CreatedOn = DateTime.UtcNow;
            //            entry.Entity.CreatedBy = httpContextAccessor.HttpContext.User.Identity.Name;
            //            break;

            //        case EntityState.Modified:
            //            entry.Entity.LastModifiedOn = DateTime.UtcNow;
            //            entry.Entity.LastModifiedBy = httpContextAccessor.HttpContext.User.Identity.Name;
            //            break;
            //    }
            //}
            return base.SaveChangesAsync(cancellationToken);
        }





        public DbSet<News> News { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategorySource> categorySources { get; set; }
    }
}
