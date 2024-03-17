using HarambeeCommerce.Persistence.Configurations;
using HarambeeCommerce.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace HarambeeCommerce.Persistence.Contexts;

public class HarambeeCommerceContext : DbContext
{
    public virtual DbSet<Customer> Customers { get; set; }
    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<Basket> Baskets { get; set; }
    public virtual DbSet<ProductBasket> ProductBaskets { get; set; }

    public HarambeeCommerceContext(DbContextOptions<HarambeeCommerceContext> options) : base(options)
    {
    }

    public HarambeeCommerceContext() : base()
    {
    }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfiguration(new BasketConfiguration());
        builder.ApplyConfiguration(new CustomerConfiguration());
        builder.ApplyConfiguration(new ProductConfiguration());
        builder.ApplyConfiguration(new ProductBasketConfiguration());
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.UseSqlServer("Data Source=localhost\\MSSQLSERVER01;Initial Catalog=ECommerce;Integrated Security=True");
    }
}
