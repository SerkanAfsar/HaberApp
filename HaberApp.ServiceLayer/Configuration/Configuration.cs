using FluentValidation;
using FluentValidation.AspNetCore;
using HaberApp.Core.DTOs.RequestDtos;
using HaberApp.Core.Services;
using HaberApp.ServiceLayer.Caching;
using HaberApp.ServiceLayer.HelperModels;
using HaberApp.ServiceLayer.Mappers;
using HaberApp.ServiceLayer.Services;
using HaberApp.ServiceLayer.Validations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ISourceService, SourceServices>();
            services.AddScoped<IRoleService, RoleService>();
            return services;
        }
        public static IServiceCollection RegisterMapper(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddAutoMapper(typeof(MappingProfile).Assembly);

            //services.AddAutoMapper(cfg => cfg.AddMaps(typeof(MappingProfile).Assembly));



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
            services.AddScoped<IValidator<CreateUserRequestDto>, CreateUserRequestDtoValidator>();
            services.AddScoped<IValidator<LoginUserRequestDto>, UserLoginRequestDtoValidator>();
            services.AddScoped<IValidator<CreateRoleRequestDto>, RoleRequestValidator>();
            return services;
        }

        public static IServiceCollection RegisterIdentityAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var settings = RegisterConfig<JwtSettings>(services, configuration.GetSection("JWTConfig"));
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = settings.ValidateIssuer,
                    ValidateAudience = settings.ValidateAudience,
                    ValidAudience = settings.ValidAudience,
                    ValidIssuer = settings.ValidIssuer,
                    ValidateIssuerSigningKey = settings.ValidateIssuerSigningKey,
                    ValidateLifetime = settings.ValidateLifetime,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.Secret)),
                    ClockSkew = TimeSpan.Zero

                };

            });
            return services;
        }
        public static TConfig RegisterConfig<TConfig>(IServiceCollection services, IConfiguration configuration) where TConfig : class, new()
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            TConfig config = new TConfig();
            services.AddSingleton<TConfig>(config);
            configuration.Bind(config);
            return config;
        }

    }
}

