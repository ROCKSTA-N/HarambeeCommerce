using HarambeeCommerce.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HarambeeCommerce.Persistence.Configurations;

public class ProductBasketConfiguration : IEntityTypeConfiguration<ProductBasket>
{
    public void Configure(EntityTypeBuilder<ProductBasket> builder)
    { 
        builder
         .HasKey(bp => new { bp.BasketId, bp.ProductId });

        builder.Property(p =>p.Count).HasDefaultValue(1);

        builder
            .HasOne(bp => bp.Basket)
            .WithMany(b => b.Products)
            .HasForeignKey(bp => bp.BasketId);

        builder
            .HasOne(bp => bp.Product)
            .WithMany(p => p.Baskets)
            .HasForeignKey(bp => bp.ProductId);
    }
}
