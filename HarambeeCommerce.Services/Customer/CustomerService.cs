using HarambeeCommerce.Persistence.Entities;
using HarambeeCommerce.Persistence.Repository;

namespace HarambeeCommerce.Services.CustomerServices
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepository<Customer> _repository;

        public CustomerService(IRepository<Customer> repository)
        {
            _repository = repository;
        }

        public async Task<Customer?> GetCustomerById(long customerId) =>
           await _repository.FindAsync(customerId);
    }
}
