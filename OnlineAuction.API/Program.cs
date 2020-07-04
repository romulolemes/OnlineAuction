using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OnlineAuction.API.Data;
using Serilog;
using Serilog.Core;
using Serilog.Extensions.Logging;
using System;
using System.IO;

namespace OnlineAuction.API
{
    public class Program
    {
        public static readonly string AppNamespace = typeof(Program).Namespace;
        public static readonly int AppId = System.Diagnostics.Process.GetCurrentProcess().Id;

        public static void Main(string[] args)
        {
            IConfiguration configuration = ConfigurationLocal.GetConfiguration();

            try
            {
                Log.Information("Configuring web host {AppName} {PID}...", AppNamespace, AppId);
                IWebHost host = BuildWebHost(configuration, args);
                MigrationDatabase(host);
                Log.Information("Starting web host {AppName} {PID}...", AppNamespace, AppId);
                host.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Program terminated unexpectedly");
            }
            finally
            {
                Log.Information("Application {AppName} has been terminated", AppNamespace);
                Log.CloseAndFlush();
            }
        }

        private static IWebHost BuildWebHost(IConfiguration configuration, string[] args)
        {
            var host = WebHost.CreateDefaultBuilder(args)
                .CaptureStartupErrors(false)
                .UseStartup<Startup>()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseConfiguration(configuration);

            ConfigureLog(configuration, host);

            return host.Build();
        }

        private static void ConfigureLog(IConfiguration configuration, IWebHostBuilder host)
        {
            host.ConfigureServices((context, collection) =>
            {
                var loggerConfiguration = new LoggerConfiguration()
                    .ReadFrom.Configuration(configuration);

                loggerConfiguration
                    .WriteTo.File($@"D:\Logs\OnlineAuction\{AppNamespace}.log", rollingInterval: RollingInterval.Day, fileSizeLimitBytes: 10000000, rollOnFileSizeLimit: true);

                Logger logger = loggerConfiguration.CreateLogger();
                Log.Logger = logger;
                collection.AddSingleton(services => (ILoggerFactory)new SerilogLoggerFactory(logger, true));
            });
        }

        private static void MigrationDatabase(IWebHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogInformation("Migrating database associated with context {DbContextName}", nameof(OnlineAuctionContext));
                try
                {
                    var context = services.GetRequiredService<OnlineAuctionContext>();
                    context.Database.Migrate();
                    logger.LogInformation("Migrated database associated with context {DbContextName}", nameof(OnlineAuctionContext));
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred while migrating the database used on context {DbContextName}", nameof(OnlineAuctionContext));
                }
            }
        }
    }
}
