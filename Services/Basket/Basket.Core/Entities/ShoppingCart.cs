namespace Basket.Core.Entities;

public class ShoppingCart
{
    public string  UserName { get; set; }
    public List<ShoppingCart> Items { get; set; } = new List<ShoppingCart>();

    public ShoppingCart()
    {
        
    }
    public ShoppingCart(string userName)
    {
        UserName= userName;
    }
}
