using HaberApp.WebService.HelperModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace HaberApp.WebService.Configuration
{
    public static class Configuration
    {
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
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.Secret))
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
