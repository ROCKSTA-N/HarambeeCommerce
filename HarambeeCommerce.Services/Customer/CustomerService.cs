using HarambeeCommerce.Persistence.Contexts;
using HarambeeCommerce.Persistence.Entities;
using HarambeeCommerce.Persistence.Repository;
using HarambeeCommerce.Services.Models;

namespace HarambeeCommerce.Services.CustomerServices;

public class CustomerService : ICustomerService
{
    private readonly HarambeeCommerceContext context;

    public CustomerService(HarambeeCommerceContext context)
    {
        this.context = context;
    }

    public async Task<CustomerDto?> GetCustomerById(long customerId)
    {
        var customer = context.Customers.FirstOrDefault(x => x.Id == customerId);

        return customer == null ? null : new CustomerDto
        {
            FirstName = customer.FirstName,
            Id = customer.Id,
            LastName = customer.LastName
        };
    }

}
