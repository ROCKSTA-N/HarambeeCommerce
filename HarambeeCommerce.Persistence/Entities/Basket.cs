namespace HarambeeCommerce.Persistence.Entities;

public class Basket : BaseEntity
{
    public long CustomerId { get; set; }
    public Customer Customer { get; set; }

    //public BasketState State { get; set; }
    public ICollection<ProductBasket> Products { get; set; }

    public decimal TotalPrice { get; set; }
}

public class ProductBasket 
{
    public long ProductId { get; set; }
    public long BasketId { get; set; }

    public int Count { get; set; }

    public Basket Basket { get; set; }
    public Product Product { get; set; }
}
