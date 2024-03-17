using HarambeeCommerce.Persistence.Entities;
using HarambeeCommerce.Services.Models;

namespace HarambeeCommerce.Services.CustomerServices;

public interface ICustomerService
{
    Task<IEnumerable<CustomerDto>> GetAllCustomersAsync();
    Task<CustomerDto?> GetCustomerById(long customerId);
}
