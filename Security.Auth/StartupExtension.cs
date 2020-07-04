using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineAuction.Security.Auth.Data;
using OnlineAuction.Security.Auth.Identify.UI;
using OnlineAuction.Security.Auth.Services;
using System.Collections.Generic;


namespace OnlineAuction.Security.Auth
{
    public static class StartupExtension
    {
        public static IServiceCollection AddLocalServices(this IServiceCollection services)
        {
            #region Transient
            services.AddTransient(typeof(AccountService));
            services.AddTransient(typeof(ConsentService));
            #endregion


            return services;
        }

        public static IServiceCollection AddIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            var sources = configuration.GetSection("Source").Get<List<string>>();

            var identityServer = services.AddIdentityServer();

            identityServer.AddInMemoryIdentityResources(InMemoryInitConfig.GetIdentityResources())
                .AddInMemoryApiResources(InMemoryInitConfig.GetApiResources())
                .AddInMemoryClients(InMemoryInitConfig.GetClients(sources))
                .AddDeveloperSigningCredential()
                .AddTestUsers(InMemoryInitConfig.GetTestUsers());

            return services;
        }
    }
}