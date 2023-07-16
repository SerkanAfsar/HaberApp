using FluentValidation;
using FluentValidation.AspNetCore;
using HaberApp.Core.DTOs.RequestDtos;
using HaberApp.Core.Services;
using HaberApp.ServiceLayer.Caching;
using HaberApp.ServiceLayer.Mappers;
using HaberApp.ServiceLayer.Services;
using HaberApp.ServiceLayer.Validations;
using Microsoft.Extensions.DependencyInjection;

namespace HaberApp.ServiceLayer.Configuration
{
    public static class Configuration
    {

        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            services.AddScoped(typeof(ICacheProcess<>), typeof(CacheProcess<>));
            services.AddScoped(typeof(IServiceBase<,,>), typeof(ServiceBase<,,>));
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICategorySourceService, CategorySourceService>();
            services.AddScoped<INewsService, NewsService>();
            return services;
        }
        public static IServiceCollection RegisterMapper(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddAutoMapper(typeof(MappingProfile).Assembly);

            return services;
        }

        public static IServiceCollection RegisterFluentValidations(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            services.AddFluentValidation(option =>
            {
                option.DisableDataAnnotationsValidation = true;
                option.RegisterValidatorsFromAssemblyContaining<CategorySourceRequestValidator>();

            });

            services.AddScoped<IValidator<CategoryRequestDto>, CategoryRequestValidator>();
            services.AddScoped<IValidator<NewsRequestDto>, NewsRequestValidator>();
            services.AddScoped<IValidator<CategorySourceRequestDto>, CategorySourceRequestValidator>();
            return services;
        }
    }
}
