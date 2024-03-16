using HarambeeCommerce.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarambeeCommerce.Services.ProductServices
{
    public interface IProductService
    {
        public Task<ICollection<Product>> GetProductsAsync();
    }
}
