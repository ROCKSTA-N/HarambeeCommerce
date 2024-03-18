using HarambeeCommerce.Services.Models;

namespace HarambeeCommerce.Services.BasketServices;

public interface IBasketService
{
    Task<BasketDto> GetBasketAsync(long id);

    Task<BasketDto> GetCustomerBasketByCustomerIdAsync(long customerId);

    Task<BasketDto> AddProductToBasketAsync(long basketId, long productId, long customerId);

    Task<decimal> CalculateBasketValue(long basketId);
}
