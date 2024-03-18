using HarambeeCommerce.Services.BasketServices;
using HarambeeCommerceApi.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace HarambeeCommerceApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BasketController : ControllerBase
{
    private readonly IBasketService _basketService;

    public BasketController(IBasketService basketService)
    {
        _basketService = basketService;
    }

    [HttpGet("{basketId}")]
    public async Task<IActionResult> GetbasketAsync(long basketId)
    { 
        var basket = await _basketService.GetBasketAsync(basketId);
        return basket is null? NoContent() : Ok(basket);
    }


    [HttpGet("calculatevalue/{basketId}")]
    public async Task<IActionResult> CalculateBasketPrice(long basketId)
    {
        var price = await _basketService.CalculateBasketValue(basketId);
        return Ok(price);
    }


    [HttpGet("customer/{customerId}")]
    public async Task<IActionResult> GetCustomerBasket(long customerId)
    {
        var basket = await _basketService.GetCustomerBasketByCustomerIdAsync(customerId);
        return basket == null ? NoContent() : Ok(basket);
    }

    [HttpPost("addProduct")]
    public async Task<IActionResult> AddProducAsync([FromBody] AddProductDto data)
    {
        var basket = await _basketService.AddProductToBasketAsync(data.BasketId, data.ProductId ,data.CustomerId);
        return Ok(basket);
    }
}
