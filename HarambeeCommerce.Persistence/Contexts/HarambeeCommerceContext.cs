using HarambeeCommerce.Persistence.Configurations;
using HarambeeCommerce.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace HarambeeCommerce.Persistence.Contexts;

public class HarambeeCommerceContext : DbContext
{
    public virtual DbSet<Customer> Customers { get; set; }
    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<Basket> Baskets { get; set; }

    public HarambeeCommerceContext(DbContextOptions<HarambeeCommerceContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfiguration(new BasketConfiguration());
        builder.ApplyConfiguration(new CustomerConfiguration());
        builder.ApplyConfiguration(new ProductConfiguration());
    }
}
