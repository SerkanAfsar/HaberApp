using HaberApp.Core.Models;
using HaberApp.Core.Repositories;
using HaberApp.Core.Repositories.NewsSourceRepositories;
using HaberApp.Core.UnitOfWork;
using HaberApp.Repository.Repositories;
using HaberApp.Repository.Repositories.NewsSourceRepositories;
using HaberApp.Repository.UnitOfWorks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HaberApp.Repository.Configuration
{
    public static class Configuration
    {
        public static IServiceCollection RegisterRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            SetConfiguration<CloudFlareSettings>(services, configuration, "CloudFlare");

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategorySourceRepository, CategorySourceRepository>();
            services.AddScoped<INewsRepository, NewsRepository>();
            services.AddScoped<IAdaletBizRepository, AdaletBizRepository>();
            services.AddScoped<IAdaletMedyaRepository, AdaletMedyaRepository>();
            services.AddSingleton<IImageHelperService, ImageHelperService>();
            return services;
        }

        public static T SetConfiguration<T>(this IServiceCollection services, IConfiguration configuration, string nodeName) where T : class, new()
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            var t = configuration.GetSection(nodeName).Get<T>();
            configuration.Bind(t);
            services.AddSingleton(configuration.GetSection(nodeName).Get<T>());

            return t;
        }
    }
}
