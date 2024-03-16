using HarambeeCommerce.Services.CustomerServices;
using HarambeeCommerce.Services.ProductServices;
using Microsoft.AspNetCore.Mvc;

namespace HarambeeCommerceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProductsAsync()
        {
            var products = await _productService.GetProductsAsync();

            return products.Any() ? NoContent() : Ok(products);
        }
    }
}
