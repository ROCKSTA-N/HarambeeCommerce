using HarambeeCommerce.Persistence.Contexts;
using HarambeeCommerce.Persistence.Entities;
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

    public async Task<CustomerDto> CreateCustomerAsync(CustomerDto customer)
    {
        var createdCustomer = await context.Customers.AddAsync(new Customer
        {
            Id = customer.Id,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            DateCreated = customer.DateCreated
        });

        await context.SaveChangesAsync();

        return new CustomerDto
        {
            FirstName = createdCustomer.Entity.FirstName,
            Id = createdCustomer.Entity.Id,
            LastName = createdCustomer.Entity.LastName,
            DateCreated = createdCustomer.Entity.DateCreated
        };
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
