namespace HarambeeCommerce.Services.Models
{
    public class ProductDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; } 
        public int Count { get; set; }
    }
}
