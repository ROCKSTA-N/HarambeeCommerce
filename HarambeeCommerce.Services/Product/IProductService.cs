using HarambeeCommerce.Persistence.Entities;

namespace HarambeeCommerce.Services.ProductServices;

public interface IProductService
{
    Task<Product?> GetProductByIdAsync(long productId);
    public Task<ICollection<Product>> GetProductsAsync();
}
