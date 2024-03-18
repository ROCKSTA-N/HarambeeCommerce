using HarambeeCommerce.Persistence.Contexts;
using HarambeeCommerce.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace HarambeeCommerce.Services.CustomerServices;

public class CustomerService : ICustomerService
{
    private readonly HarambeeCommerceContext context;

    public CustomerService(HarambeeCommerceContext context)
    {
        this.context = context;
    }

    public IEnumerable<CustomerDto> GetAllCustomers()
    {
        var customers = context.Customers;
         

        return customers.ToList().Select(customer => new CustomerDto
            {
                FirstName = customer.FirstName,
                Id = customer.Id,
                LastName = customer.LastName,
                DateCreated = customer.DateCreated
            }).ToList();
    }

    public async Task<CustomerDto?> GetCustomerById(long customerId)
    {
        var customer = await context.Customers.FirstOrDefaultAsync(x => x.Id == customerId);

        return customer == null ? null : new CustomerDto
        {
            FirstName = customer.FirstName,
            Id = customer.Id,
            LastName = customer.LastName,
            DateCreated = customer.DateCreated
        };
    }

}
