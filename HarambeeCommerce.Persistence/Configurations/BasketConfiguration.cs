using HarambeeCommerce.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace HarambeeCommerce.Persistence.Configurations;

public class BasketConfiguration : IEntityTypeConfiguration<Basket>
{
    public void Configure(EntityTypeBuilder<Basket> builder)
    {
        builder
            .ToTable(nameof(Basket))
            .HasKey(x => x.Id);

        builder.HasOne(p => p.Customer);

        builder.Property(x => x.Id).UseIdentityColumn();
        builder.Property(x => x.TotalPrice).HasDefaultValue(0);
        builder.Property(x => x.DateCreated).HasDefaultValue(DateTime.Now);
        builder
           .HasMany(b => b.Products)
           .WithOne(bp => bp.Basket)
           .HasForeignKey(bp => bp.BasketId);
    }
}
