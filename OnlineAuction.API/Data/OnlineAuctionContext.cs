using BaseAPI.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using OnlineAuction.API.Models;

namespace OnlineAuction.API.Data
{
    public class OnlineAuctionContext : DbContext
    {
        public OnlineAuctionContext()
        {
        }

        public OnlineAuctionContext(DbContextOptions<OnlineAuctionContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AuctionModel> Auction { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var configuration = ConfigurationLocal.GetConfiguration();
                var baseSettings = new BaseSettings();
                configuration.Bind(baseSettings);
                optionsBuilder.UseSqlServer(baseSettings.ConnectionStrings.OnlineAuctionContext);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
