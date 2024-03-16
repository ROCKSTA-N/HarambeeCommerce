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

    [HttpPost("addProduct")]
    public async Task<IActionResult> CalculateBasketPrice([FromBody] AddProductDto data)
    {
        var basket = await _basketService.AddProductToBasketAsync(data.BasketId, data.ProductId);
        return Ok(basket);
    }
}
