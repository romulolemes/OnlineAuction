using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using OnlineAuction.Security.Auth.Data;
using OnlineAuction.Security.Auth.Identify.UI;
using System.Collections.Generic;

namespace OnlineAuction.Security.Auth
{
    public class Startup
    {
        public const string CorsPolicyOrigins = "_CorsPolicyOrigins";

        private IHostingEnvironment Env { get; }
        private IConfiguration Configuration { get; }



        public Startup(IHostingEnvironment env, IConfiguration configuration)
        {
            Env = env;
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCustomOptions(Configuration)
                .AddLocalServices()
                .AddCustomHealthCheck()
                .AddCustomMVC(Configuration)
                .AddIdentity(Configuration, Env);
        }

        public void Configure(IApplicationBuilder app)
        {
            if (Env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();

            }

            // Make work identity server redirections in Edge and lastest versions of browers. WARN: Not valid in a production environment.
            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add("Content-Security-Policy", "script-src 'unsafe-inline'");
                await next();
            });

            app.UseIdentityServer();
            app.UseStaticFiles();
            app.UseCors(CorsPolicyOrigins);

            app.UseMvcWithDefaultRoute();

            app.UseHealthChecks("/hc", new HealthCheckOptions()
            {
                Predicate = _ => true,
                ResponseWriter = HealthChecks.UI.Client.UIResponseWriter.WriteHealthCheckUIResponse
            });

            app.UseHealthChecks("/liveness", new HealthCheckOptions
            {
                Predicate = r => r.Name.Contains("self")
            });
        }
    }

    public static class ExtensionMethods
    {
        public static IServiceCollection AddLocalServices(this IServiceCollection services)
        {
            #region Transient
            services.AddTransient(typeof(AccountService));
            services.AddTransient(typeof(ConsentService));
            #endregion


            return services;
        }

        public static IServiceCollection AddCustomHealthCheck(this IServiceCollection services)
        {
            var hcBuilder = services.AddHealthChecks();

            hcBuilder
                .AddCheck("self", () => HealthCheckResult.Healthy());

            return services;
        }

        public static IServiceCollection AddCustomMVC(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(Startup.CorsPolicyOrigins,
                x =>
                {
                    x.SetIsOriginAllowed((host) => true)
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
                });
            });


            services.AddMvc()
            .AddJsonOptions(opttionsJson =>
            {
                opttionsJson.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                opttionsJson.SerializerSettings.ContractResolver = new DefaultContractResolver();
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            return services;
        }

        public static IServiceCollection AddIdentity(this IServiceCollection services, IConfiguration configuration, IHostingEnvironment env)
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

        public static IServiceCollection AddCustomOptions(this IServiceCollection services, IConfiguration configuration)
        {
            _ = services.Configure<AppSettings>(configuration);

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var problemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Instance = context.HttpContext.Request.Path,
                        Status = StatusCodes.Status400BadRequest,
                        Detail = "Please refer to the errors property for additional details."
                    };

                    return new BadRequestObjectResult(problemDetails)
                    {
                        ContentTypes = { "application/problem+json", "application/problem+xml" }
                    };
                };
            });

            return services;
        }
    }
}