using HarambeeCommerce.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarambeeCommerce.Services.CustomerServices
{
    public interface ICustomerService
    {
        Task<Customer> GetCustomerById(long customerId);
    }
}
