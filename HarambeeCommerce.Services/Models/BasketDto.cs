using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarambeeCommerce.Services.Models
{
    public class BasketDto
    {
        public long Id { get; set; } 
        public decimal TotalPrice { get; set; }

        public IEnumerable<ProductDto> Products { get; set; }
        public CustomerDto Customer { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
