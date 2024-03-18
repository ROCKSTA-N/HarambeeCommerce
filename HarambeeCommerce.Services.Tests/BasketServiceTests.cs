using Castle.Core.Resource;
using FluentAssertions;
using HarambeeCommerce.Persistence.Contexts;
using HarambeeCommerce.Persistence.Entities;
using HarambeeCommerce.Services.BasketServices;
using Moq;
using System.Linq.Expressions;
using Xunit;
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

       
        public async Task Test_AddProductToBasket_GiveRegisteredProductWith1CountInStock_TotalPriceOfbasketShouldMatchProductPrice()
        {  ;

            var context = new Mock<HarambeeCommerceContext>();

            context.Setup(m => m.Baskets.FindAsync(It.IsAny<long>())).ReturnsAsync(new Basket { 
                 Products = Array.Empty<ProductBasket>()
            });

            context.Setup(m => m.ProductBaskets.SkipWhile(It.IsAny<Expression<Func<ProductBasket, bool>>>())).Returns(new List<ProductBasket>()
            {
                new ProductBasket {
                    Product = new Product { CountInStock = 1, Price = 100 }
                }
            }.AsQueryable());
            context.Setup(m => m.Customers.FindAsync(It.IsAny<long>())).ReturnsAsync(new Customer());
            context.Setup(m => m.Products.FindAsync(It.IsAny<long>())).ReturnsAsync(new Product { CountInStock = 1, Price = 100 });
            var service = new BasketService(context.Object);

            var basket = await service.AddProductToBasketAsync(1, 1, 1);

            basket.TotalPrice.Should().Be(100);
        }
    }
}