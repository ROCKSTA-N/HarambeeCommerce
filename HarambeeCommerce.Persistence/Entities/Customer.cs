namespace HarambeeCommerce.Persistence.Entities;

public class Customer : BaseEntity
{ 
    public string FirstName { get; set; }
    public string LastName { get; set; }

    //public ICollection<Basket> Baskets { get; set; }
}
