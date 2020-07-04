using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace OnlineAuction.API
{
    public static class StartupExtension
    {
        public static IServiceCollection AddConfigureAuthService(this IServiceCollection services, IConfiguration configuration)
        {
            var identityUrl = "http://localhost:5101";

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
           .AddJwtBearer(options =>
           {
               options.Authority = identityUrl;
               options.Audience = "onlineauction";
               options.RequireHttpsMetadata = false;
           });

            services.AddAuthorization();

            return services;
        }
    }
}
