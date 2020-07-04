using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineAuction.API
{
    public class ConfigurationLocal
    {
        public static IConfiguration GetConfiguration()
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
            //configuration.Bind(AppSettings.Start);
            return configuration;
        }
    }
}
