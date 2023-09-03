using HaberApp.Core.Repositories;
using HaberApp.Core.Repositories.NewsSourceRepositories;
using HaberApp.Core.UnitOfWork;
using HaberApp.Repository.Repositories;
using HaberApp.Repository.Repositories.NewsSourceRepositories;
using HaberApp.Repository.UnitOfWorks;
using Microsoft.Extensions.DependencyInjection;

namespace HaberApp.Repository.Configuration
{
    public static class Configuration
    {
        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategorySourceRepository, CategorySourceRepository>();
            services.AddScoped<INewsRepository, NewsRepository>();
            services.AddScoped<IAdaletBizRepository, AdaletBizRepository>();
            return services;
        }
    }
}
