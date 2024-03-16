namespace HarambeeCommerce.Persistence.Entities;

public class Basket : BaseEntity
{
    public long CustomerId { get; set; }
    public Customer Customer { get; set; }

    public BasketState State { get; set; }
    public ICollection<Product> Products { get; set; }

    public decimal? Price { get; set; }
    public decimal? TotalPrice { get; set; }
    public decimal DiscountPercentage { get; set; }
}
