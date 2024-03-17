using HarambeeCommerce.Persistence.Contexts;
using HarambeeCommerce.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace HarambeeCommerce.Services.Tests.Utils
{
    public class MockHarambeeCommerceContext : IHarambeeCommerceContext
    {
        public MockHarambeeCommerceContext()
        {
            Customers = DbSetMockHelper.GetQueryableMockDbSet(new List<Customer>());
            Products = DbSetMockHelper.GetQueryableMockDbSet(new List<Product>());
            Baskets = DbSetMockHelper.GetQueryableMockDbSet(new List<Basket>());
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<ProductBasket> ProductBaskets { get; set; }


        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => Task.FromResult(0);
    }
}