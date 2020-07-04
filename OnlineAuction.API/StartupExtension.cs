using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineAuction.API.Services;

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

        public static IServiceCollection AddLocalServices(this IServiceCollection services)
        {
            #region Transient
            services.AddTransient(typeof(AuctionService));
            #endregion


            return services;
        }
    }
}
