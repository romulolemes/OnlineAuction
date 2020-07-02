using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Core;
using Serilog.Extensions.Logging;
using System;
using System.IO;

namespace OnlineAuction.Security.Auth
{
    /// <summary>
    /// Classe principal.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Namespace da aplicação.
        /// </summary>
        public static readonly string AppNamespace = typeof(Program).Namespace;
        /// <summary>
        /// Nome da aplicação.
        /// </summary>
        public static readonly string AppName = "Security.Auth";
        /// <summary>
        /// Chave de acesso a string de conexão padrão com banco de dados.
        /// </summary>
        public static readonly string DefaultConnectionKey = "DefaultConnection";
        /// <summary>
        /// Id da aplicação no sistema operacional.
        /// </summary>
        public static readonly int AppId = System.Diagnostics.Process.GetCurrentProcess().Id;

        /// <summary>
        /// Método principal inicial.
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            IConfiguration configuration = GetConfiguration();

            try
            {
                Log.Information("Configuring web host {AppName} {PID}...", AppName, AppId);
                IWebHost host = BuildWebHost(configuration, args);

                Log.Information("Starting web host {AppName} {PID}...", AppName, AppId);
                host.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Program terminated unexpectedly");
            }
            finally
            {
                Log.Information("Application {AppName} has been terminated", AppName);
                Log.CloseAndFlush();
            }
        }

        /// <summary>
        /// Configuração do WebHost.
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="args"></param>
        /// <returns></returns>
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
                    .WriteTo.File($@"D:\Logs\OnlineAuction\{AppName}.log", rollingInterval: RollingInterval.Day, fileSizeLimitBytes: 10000000, rollOnFileSizeLimit: true);

                Logger logger = loggerConfiguration.CreateLogger();
                Log.Logger = logger;
                collection.AddSingleton(services => (ILoggerFactory)new SerilogLoggerFactory(logger, true));
            });
        }

        /// <summary>
        /// Recupera as configurações.
        /// </summary>
        /// <returns></returns>
        private static IConfiguration GetConfiguration()
        {
            // Get configuration
            var builder = new ConfigurationBuilder();
            string currentDirectory = Directory.GetCurrentDirectory();
            builder
                .AddJsonFile(Path.Combine(currentDirectory, "appsettings.json"), optional: false, reloadOnChange: true)
                .AddJsonFile(Path.Combine(currentDirectory, $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development"}.json"), optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
            // Bind settings
            var configuration = builder.Build();
            configuration.Bind(AppSettings.Start);
            return configuration;
        }
    }
}
