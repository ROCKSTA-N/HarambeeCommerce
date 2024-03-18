using FluentAssertions;
using HarambeeCommerce.Persistence.Contexts;
using HarambeeCommerce.Persistence.Entities;
using HarambeeCommerce.Services.BasketServices;
using HarambeeCommerce.Services.Models;
using HarambeeCommerceApi.Controllers;
using HarambeeCommerceApi.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace HarambeeCommerceApi.Tests
{
    public class BasketControllerTests
    {
        private readonly HarambeeCommerceContext _context;

        public BasketControllerTests()
        {

            var contextOptions = new DbContextOptionsBuilder<HarambeeCommerceContext>()
                .UseInMemoryDatabase("ECommerce");

            _context = new HarambeeCommerceContext(contextOptions.Options); ;
        }

        [Fact]
        public async Task Test_GetBasketAsync_WithNoRegisteredBasketAsync_ShouldReturn_NoContentResult()
        {
            // this test uses an in-memory db due to limitations in moq ,  i can not mock methods like include
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            var service = new BasketService(_context); 

            var basketController = new BasketController(service);

            var response = await basketController.GetbasketAsync(1);

            var result = response as NoContentResult;

            result.StatusCode.Should().Be(204);
        }


        [Fact]
        public async Task Test_GetBasketAsync_WithRegisteredBasketAsync_ShouldReturn_OkResult()
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
            _context.Baskets.Add(new Basket
            {
                CustomerId = 1,
                Products = new List<ProductBasket>()
                {
                     new ProductBasket
                     {
                           ProductId = 1,
                           BasketId = 1,
                           Count = 1,
                           Product = new Product
                            {
                                Price = 10,
                                Description = "Test",
                                Name = "Test",
                                CountInStock = 10
                            }
                     }
                }
            });

           await _context.SaveChangesAsync();
             

            var service = new BasketService(_context);

            var basketController = new BasketController(service);

            var response = await basketController.GetbasketAsync(1);

            var result = response as OkObjectResult;

            result.StatusCode.Should().Be(200);
            result.Value.Should().BeAssignableTo<BasketDto>();
        }

        [Fact]
        public async Task Test_CalculateBasketValueAsync_WithNoRegisteredBasketAsync_ShouldReturn_OkWithZeroValue()
        {
            // this test uses an in-memory db due to limitations in moq ,  i can not mock methods like include
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            var service = new BasketService(_context);

            var basketController = new BasketController(service);

            var response = await basketController.CalculateBasketPrice(1);

            var result = response as OkObjectResult;

            result.StatusCode.Should().Be(200);
            result.Value.Should().Be(0M);
        }

        [Fact]
        public async Task Test_CalculateBasketValueAsync_WithRegisteredBasketAsync_ShouldReturn_OkWith10Value()
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

            await _context.SaveChangesAsync();


            var service = new BasketService(_context);

            var basketController = new BasketController(service);

            await basketController.AddProducAsync(new AddProductDto
            {
                BasketId = 0,
                CustomerId = 1,
                ProductId = 1
            });

            var response = await basketController.CalculateBasketPrice(1);
            var result = response as OkObjectResult;

            result.StatusCode.Should().Be(200);
            result.Value.Should().Be(10M);
        }
    }
}