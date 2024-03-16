using HarambeeCommerce.Services.BasketServices;
using Microsoft.AspNetCore.Mvc;

namespace HarambeeCommerceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }
    }
}
