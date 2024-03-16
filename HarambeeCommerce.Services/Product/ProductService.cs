using HarambeeCommerce.Persistence.Entities;
using HarambeeCommerce.Persistence.Repository;

namespace HarambeeCommerce.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _repository;

        public ProductService(IRepository<Product> repository)
        {
            _repository = repository;
        }

        public async Task<Product?> GetProductByIdAsync(long productId) => await _repository.FindAsync(productId);

        public async Task<ICollection<Product>> GetProductsAsync() => await _repository.GetAllAsync();
    }
}
