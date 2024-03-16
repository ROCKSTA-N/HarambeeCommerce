using HarambeeCommerce.Persistence.Entities;

namespace HarambeeCommerce.Services.CustomerServices;

public interface ICustomerService
{
    Task<Customer?> GetCustomerById(long customerId);
}
