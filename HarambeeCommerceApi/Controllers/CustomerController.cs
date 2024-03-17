using HarambeeCommerce.Persistence.Entities;
using HarambeeCommerce.Services.CustomerServices;
using Microsoft.AspNetCore.Mvc;

namespace HarambeeCommerceApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomerController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCustomers()
    {
        var customers = await _customerService.GetAllCustomersAsync();
        return !customers.Any() ? NoContent() : Ok(customers);
    }

    [HttpGet("{customerId}")]
    public async Task<IActionResult> GetCustomerByIdAsync(long customerId) 
    {
        var customer = await _customerService.GetCustomerById(customerId);

        return customer is null ? NoContent() : Ok(customer);
    }
}
