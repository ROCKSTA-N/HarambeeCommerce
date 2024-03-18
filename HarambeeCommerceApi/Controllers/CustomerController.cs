using HarambeeCommerce.Services.CustomerServices;
using HarambeeCommerce.Services.Models;
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
    public  IActionResult GetAllCustomers()
    {
        var customers =  _customerService.GetAllCustomers();
        return !customers.Any() ? NoContent() : Ok(customers);
    }

    [HttpGet("{customerId}")]
    public async Task<IActionResult> GetCustomerByIdAsync(long customerId) 
    {
        var customer = await _customerService.GetCustomerById(customerId);

        return customer is null ? NoContent() : Ok(customer);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCustomerAsync([FromBody] CustomerDto customer)
    {
        var newCustomer = await _customerService.CreateCustomerAsync(customer);

        return customer is null ? NoContent() : Ok(newCustomer);
    }
}
