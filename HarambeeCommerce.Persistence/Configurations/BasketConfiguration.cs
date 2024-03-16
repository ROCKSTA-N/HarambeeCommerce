using HarambeeCommerce.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HarambeeCommerce.Persistence.Configurations;

public class BasketConfiguration : IEntityTypeConfiguration<Basket>
{
    public void Configure(EntityTypeBuilder<Basket> builder)
    {
        builder
            .ToTable(nameof(Basket)) 
            .HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityColumn();
        builder
            .Property(x => x.State)
            .HasDefaultValue(BasketState.Active);

        builder.HasOne(x => x.Customer)
            .WithMany(x =>x.Baskets)
            .HasForeignKey(x => x.CustomerId);
    }
}
