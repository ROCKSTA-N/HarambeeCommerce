using HarambeeCommerce.Services.Models;

namespace HarambeeCommerce.Services.CustomerServices;

public interface ICustomerService
{
    Task<CustomerDto> CreateCustomerAsync(CustomerDto customer);

    IEnumerable<CustomerDto> GetAllCustomers();
    Task<CustomerDto?> GetCustomerById(long customerId);
}
