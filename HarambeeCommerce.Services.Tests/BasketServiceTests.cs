using FluentAssertions;
using HarambeeCommerce.Persistence.Contexts;
using HarambeeCommerce.Persistence.Entities;
using HarambeeCommerce.Services.BasketServices;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Linq.Expressions;
using System.Reflection.Metadata;

namespace HarambeeCommerce.Services.Tests
{
    public class BasketServiceTests
    {
        private readonly HarambeeCommerceContext _context;

        public BasketServiceTests()
        {

            var contextOptions = new DbContextOptionsBuilder<HarambeeCommerceContext>()
                .UseInMemoryDatabase("ECommerce");

            _context = new HarambeeCommerceContext(contextOptions.Options);;
        }

        [Fact]
        public async Task Test_AddProductToBasket_GiveNonRegisteredUser_ThrowInvalidOperationExceptionAsync()
        {
            var context = new Mock<HarambeeCommerceContext>();
            context.Setup(m => m.Customers.FindAsync(It.IsAny<long>())).Returns(null);

            var service = new BasketService(context.Object);

            var act = async () => await service.AddProductToBasketAsync(1, 1, 1);
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(act);
            exception.Message.Should().BeEquivalentTo("Customer is required !");
        }

        [Fact]
        public async Task Test_AddProductToBasket_GiveNonRegisteredProduct_ThrowInvalidOperationExceptionAsync()
        {
            var context = new Mock<HarambeeCommerceContext>();
            context.Setup(m => m.Customers.FindAsync(It.IsAny<long>())).ReturnsAsync(new Customer());
            context.Setup(m => m.Products.FindAsync(It.IsAny<long>())).Returns(null);
            var service = new BasketService(context.Object);

            var act = async () => await service.AddProductToBasketAsync(1, 1, 1);
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(act);
            exception.Message.Should().BeEquivalentTo("You can not add a product that does not exist !");
        }

        [Fact]
        public async Task Test_AddProductToBasket_GiveRegisteredProductWith0CountInStock_ThrowInvalidOperationExceptionAsync()
        {
            var context = new Mock<HarambeeCommerceContext>();
            context.Setup(m => m.Customers.FindAsync(It.IsAny<long>())).ReturnsAsync(new Customer());
            context.Setup(m => m.Products.FindAsync(It.IsAny<long>())).ReturnsAsync(new Product { CountInStock = 0 });
            var service = new BasketService(context.Object);

            var act = async () => await service.AddProductToBasketAsync(1, 1, 1);
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(act);
            exception.Message.Should().BeEquivalentTo("Product out of stock");
        }

        [Fact]
        public async Task Test_AddProductToBasket_GiveRegisteredProductWith1CountInStock_TotalPriceOfbasketShouldMatchProductPrice()
        {
            // this test uses an in-memory db due to limitations in moq ,  i can not mock methods like include
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
            _context.Products.Add(new Product
            {
                Price = 10,
                Description = "Test",
                Name = "Test",
                CountInStock = 10
            });

            _context.Customers.Add(new Customer
            {
                FirstName = "John",
                LastName = "Doe",
                DateCreated = DateTime.Now
            });

            _context.SaveChanges();

            var service = new BasketService(_context);

            var basket = await service.AddProductToBasketAsync(0, 1, 1);

            basket.TotalPrice.Should().Be(10);
        }

        [Fact]
        public async Task Test_AddProductToBasket_GiveRegisteredProductWith1CountInStock_TotalPriceOfbasketShouldDiscount5Perce()
        {
            // this test uses an in-memory db due to limitations in moq ,  i can not mock methods like include
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated(); 
            
            _context.Products.Add(new Product
            {
                Price = 50,
                Description = "Test",
                Name = "Test",
                CountInStock = 10
            });

            _context.Products.Add(new Product
            {
                Price = 70,
                Description = "Test 2",
                Name = "Test 2",
                CountInStock = 1
            });

            _context.Customers.Add(new Customer
            {
                FirstName = "John",
                LastName = "Doe",
                DateCreated = DateTime.Now
            });

            _context.Baskets.Add(new Basket
            {
                CustomerId = 1,
                Products = new List<ProductBasket>()
                {
                     new ProductBasket
                     {
                           ProductId = 1,
                           BasketId = 1,
                           Count = 9
                     },
                     new ProductBasket
                     {
                        ProductId = 2,
                        BasketId = 1,
                        Count = 1
                     }
                }
            });

            _context.SaveChanges();

            var service = new BasketService(_context);

            var basket = await service.AddProductToBasketAsync(1, 1, 1);

            basket.TotalPrice.Should().Be(541.5M);
        }
    }
}