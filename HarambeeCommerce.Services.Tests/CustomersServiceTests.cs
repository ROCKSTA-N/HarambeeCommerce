using FluentAssertions;
using HarambeeCommerce.Persistence.Contexts;
using HarambeeCommerce.Persistence.Entities;
using HarambeeCommerce.Services.CustomerServices;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace HarambeeCommerce.Services.Tests
{
    public class CustomersServiceTests
    {
        
        public void Test_GetAllCustomersWhenNoCustomerIsRegistered_ShouldReturnEmptyList()
        {
            var customerSet = new Mock<DbSet<Customer>>();
           
            var context = new Mock<HarambeeCommerceContext>();
            context.Setup(m => m.Customers).Returns(customerSet.Object);

            var service = new CustomerService(context.Object);

            var customers =  service.GetAllCustomers();

            customers.Should().BeEmpty();
        }
    }
}