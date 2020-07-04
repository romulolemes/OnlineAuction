using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineAuction.API.Models;

namespace OnlineAuction.API.Data.Config
{
    public class AuctionConfig : IEntityTypeConfiguration<AuctionModel>
    {
        public void Configure(EntityTypeBuilder<AuctionModel> builder)
        {
            builder.ToTable("Auction");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(p => p.User)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(p => p.InitialValue)
                .HasColumnType("decimal(18, 2)");
        }
    }
}
