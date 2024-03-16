using HarambeeCommerce.Persistence.Entities;
using HarambeeCommerce.Persistence.Repository;
using HarambeeCommerce.Services.ProductServices;

namespace HarambeeCommerce.Services.BasketServices;

public class BasketService : IBasketService
{
    private readonly IRepository<Basket> _basketRepository;
    private readonly IProductService _productService;

    public BasketService(IRepository<Basket> basketRepository, IProductService productService)
    {
        _basketRepository = basketRepository;
        _productService = productService;
    }

    public async Task<Basket> AddProductToBasketAsync(long? basketId, long productId)
    {
        var product = await _productService.GetProductByIdAsync(productId) 
                    ?? throw new InvalidOperationException("You can not add a product that does not exist !");

        var basket = !basketId.HasValue ? new Basket() : await GetBasketAsync(basketId.Value);

        if (!basket.Products.Any())
            basket.Products = Array.Empty<Product>();

        basket.Products.Add(product);

        basket = basketId.HasValue ?  _basketRepository.Update(basket) : await _basketRepository.AddAsync(basket);
        await _basketRepository.SaveAsync();

        return basket;
    }

    public async Task<decimal> CalculateBasketValue(long basketId)
    {
        var basket =  await GetBasketAsync(basketId);
        return basket is null ? 0M : basket.Products.Sum(x => x.Price);
    }

    public async Task<Basket> GetBasketAsync(long id) => await _basketRepository.FindAsync(id);  

    public async Task<Basket?> GetCustomerBasketByCustomerIdAsync(long customerId) => await _basketRepository.QuerySingle(x =>x.CustomerId == customerId && x.State == BasketState.Active);
  
}
