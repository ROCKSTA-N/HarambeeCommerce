using HarambeeCommerce.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarambeeCommerce.Services.BasketServices
{
    public interface IBasketService
    {
        Task<Basket> GetBasketAsync(long id);

        Task<Basket> GetCustomerBasketByCustomerIdAsync(long customerId);

        Task<Basket> AddProductToBasketAsync(long? basketId, long productId);

        Task<decimal> CalculateBasketValue(long basketId);
    }
}
