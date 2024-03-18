using HarambeeCommerce.Persistence.Contexts;
using HarambeeCommerce.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace HarambeeCommerce.Services.ProductServices;

public class ProductService : IProductService
{
    private readonly HarambeeCommerceContext _context;

    public ProductService(HarambeeCommerceContext context)
    {
        _context = context;
    }

    public async Task<ProductDto?> GetProductByIdAsync(long productId)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);

        return product == null ? null : new ProductDto
        {
            Count = product.CountInStock,
            Id = product.Id,
            Description = product.Description,
            Name = product.Name,
            Price = product.Price,
            DateCreated = product.DateCreated,
        };
    }

    public async Task<ICollection<ProductDto>> GetProductsAsync()
    {
        var products = await _context.Products.ToListAsync();

        return products.Any() ? products.Select(product => new ProductDto
        {
            Count = product.CountInStock,
            Id = product.Id,
            Description = product.Description,
            Name = product.Name,
            Price = product.Price,
            DateCreated = product.DateCreated,
        }).ToList() : Array.Empty<ProductDto>();
    }
}
