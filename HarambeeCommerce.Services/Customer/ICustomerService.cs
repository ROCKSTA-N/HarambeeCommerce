using HarambeeCommerce.Persistence.Entities;
using HarambeeCommerce.Services.Models;

namespace HarambeeCommerce.Services.CustomerServices;

public interface ICustomerService
{
    IEnumerable<CustomerDto> GetAllCustomers();
    Task<CustomerDto?> GetCustomerById(long customerId);
}
