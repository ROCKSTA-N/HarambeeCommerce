using HarambeeCommerce.Persistence.Entities;
using HarambeeCommerce.Services.Models;

namespace HarambeeCommerce.Services.CustomerServices;

public interface ICustomerService
{
    Task<CustomerDto?> GetCustomerById(long customerId);
}
