using HarambeeCommerce.Persistence.Contexts;
using HarambeeCommerce.Persistence.Entities;
using HarambeeCommerce.Persistence.Repository;
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

    public async Task<IEnumerable<CustomerDto>> GetAllCustomersAsync()
    {
        var customers = await context.Customers.ToListAsync();
        return !customers.Any() ? Array.Empty<CustomerDto>().ToList() : 
            customers.Select(customer => new CustomerDto
            {
                FirstName = customer.FirstName,
                Id = customer.Id,
                LastName = customer.LastName
            }).ToList();
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
