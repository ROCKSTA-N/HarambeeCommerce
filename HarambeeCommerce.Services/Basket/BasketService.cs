using HarambeeCommerce.Persistence.Contexts;
using HarambeeCommerce.Persistence.Entities;
using HarambeeCommerce.Persistence.Repository;
using HarambeeCommerce.Services.Models;
using HarambeeCommerce.Services.ProductServices;
using Microsoft.EntityFrameworkCore;

namespace HarambeeCommerce.Services.BasketServices;

public class BasketService : IBasketService
{
    private readonly HarambeeCommerceContext harambeeCommerceContext;

    public BasketService(HarambeeCommerceContext harambeeCommerceContext)
    {
        this.harambeeCommerceContext = harambeeCommerceContext;
    }

    public async Task<BasketDto> AddProductToBasketAsync(long basketId, long productId, long customerId)
    {
        var customer = await harambeeCommerceContext.Customers.FindAsync(customerId)
                    ?? throw new InvalidOperationException("Customer is required !");

        var product = await harambeeCommerceContext.Products.FindAsync(productId)
                    ?? throw new InvalidOperationException("You can not add a product that does not exist !");

        if(product.CountInStock == 0)
            throw new InvalidOperationException("Product out of stock");

        var basket = new Basket();

        if (basketId == 0)
        {
            basket = new Basket
            {
                CustomerId = customerId,
                Products = Array.Empty<ProductBasket>()
            };
            var entityState = await harambeeCommerceContext.Baskets.AddAsync(basket);
            basket = entityState.Entity;
        }
        else
        {

            basket = await harambeeCommerceContext.Baskets
                    .FirstAsync(basket => basket.CustomerId == customerId) ?? throw new InvalidOperationException("Basket not found");

            basket.Products = await AttachProductsAsync(basket.Id);
        }

        if (basket.Products.Any(p => p.ProductId == productId))
        {
            basket.Products.First(p => p.ProductId == productId).Count += 1; 
        }
        else
        {
            basket.Products.Add(new ProductBasket
            {
                BasketId = basket.Id,
                ProductId = productId,
            });

        }

        basket.TotalPrice += product.Price;

        product.CountInStock -= 1;

        basket.TotalPrice = CalculatePriceDiscount(basket.TotalPrice,basket.Products);

        await harambeeCommerceContext.SaveChangesAsync();
        return new BasketDto
        {
            Customer = new CustomerDto { Id = customer.Id, FirstName = customer.FirstName, LastName = customer.LastName , DateCreated = customer.DateCreated},
            Products = basket.Products.Select(p => new ProductDto {
                Count= p.Count, Id = p.Product.Id, Description = p.Product.Description, Name = p.Product.Name, Price = p.Product.Price,
                DateCreated = p.DateCreated
            }),
            Id = basket.Id, 
            TotalPrice = basket.TotalPrice,
            DateCreated = basket.DateCreated
        };
    }

    private async Task<ICollection<ProductBasket>> AttachProductsAsync(long id)
    {
        var productsInBasket = harambeeCommerceContext.ProductBaskets.SkipWhile(x =>x.BasketId !=  id).ToList();
        foreach (var pb in productsInBasket)
            pb.Product = await harambeeCommerceContext.Products.FindAsync(pb.ProductId);
        return productsInBasket;
    }

    private decimal CalculatePriceDiscount(decimal price, ICollection<ProductBasket> products)
    {
        var discountPercentage = 0.5M;
        var productCount = products.Sum( p => p.Count) ;

        return productCount < 5 ? price : (price * discountPercentage) + price;
    }

    public async Task<decimal> CalculateBasketValue(long basketId)
    {
        var basket = await GetBasketAsync(basketId);
        return basket is null ? 0M : basket.TotalPrice;
    }

    public async Task<BasketDto> GetBasketAsync(long id)
    {
        var basket = await harambeeCommerceContext
                    .Baskets.Include(p => p.Customer)
                    .Include(p => p.Products).ThenInclude(bp => bp.Product)
                    .FirstAsync(basket => basket.Id == id);

        var customer = basket.Customer;
        return new BasketDto
        {
            Customer = new CustomerDto { Id = customer.Id, FirstName = customer.FirstName, LastName = customer.LastName, DateCreated = customer.DateCreated },
            Products = basket.Products.Select(p => new ProductDto
            {
                Count = p.Count,
                Id = p.Product.Id,
                Description = p.Product.Description,
                Name = p.Product.Name,
                Price = p.Product.Price,
                DateCreated = p.DateCreated
            }),
            Id = basket.Id,
            TotalPrice = basket.TotalPrice,
            DateCreated = basket.DateCreated
        };
    }

    public async Task<BasketDto> GetCustomerBasketByCustomerIdAsync(long customerId)
    {
        var basket = await harambeeCommerceContext
                    .Baskets.Include(p => p.Customer)
                    .Include(p => p.Products).ThenInclude(bp => bp.Product)
                    .FirstAsync(basket => basket.CustomerId == customerId);

        var customer = basket.Customer;
        return new BasketDto
        {
            Customer = new CustomerDto { Id = customer.Id, FirstName = customer.FirstName, LastName = customer.LastName, DateCreated = customer.DateCreated },
            Products = basket.Products.Select(p => new ProductDto
            {
                Count = p.Count,
                Id = p.Product.Id,
                Description = p.Product.Description,
                Name = p.Product.Name,
                Price = p.Product.Price,
                DateCreated = p.DateCreated
            }),
            Id = basket.Id,
            TotalPrice = basket.TotalPrice,
            DateCreated = basket.DateCreated
        };
    }
}
