using HarambeeCommerce.Services.Models;

namespace HarambeeCommerce.Services.ProductServices;

public interface IProductService
{
    Task<ProductDto> GetProductByIdAsync(long productId);
    public Task<ICollection<ProductDto>> GetProductsAsync();
}
