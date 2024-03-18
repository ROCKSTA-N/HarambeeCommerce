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
            context.Setup(m => m.Products.FindAsync(It.IsAny<long>())).ReturnsAsync(new Product { CountInStock = 0});
            var service = new BasketService(context.Object);

            var act = async () => await service.AddProductToBasketAsync(1, 1, 1);
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(act);
            exception.Message.Should().BeEquivalentTo("Product out of stock");
        }

        [Fact]
        public async Task Test_AddProductToBasket_GiveRegisteredProductWith1CountInStock_TotalPriceOfbasketShouldMatchProductPrice()
        { 

            var contextOptions = new DbContextOptionsBuilder<HarambeeCommerceContext>()
                .UseInMemoryDatabase("ECommerce");

            using var context = new HarambeeCommerceContext(contextOptions.Options);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.Products.Add(new Product
            {
                Price = 10,
                Description = "Test",
                Name = "Test",
                CountInStock = 10
            });

            context.Customers.Add(new Customer
            {
                FirstName = "John",
                LastName = "Doe",
                DateCreated = DateTime.Now
            });

            context.SaveChanges();

            var service = new BasketService(context);

            var basket = await service.AddProductToBasketAsync(0, 1, 1);

            basket.TotalPrice.Should().Be(10);
        }
    }
}