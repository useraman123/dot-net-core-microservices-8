using Basket.Core.Entities;

namespace Basket.Application.Reponses;

public class ShoppingCartResponse
{
    public string UserName { get; set; }
    public List<ShoppingCartItemResponse> Items { get; set; }
    public ShoppingCartResponse()
    {
        
    }
    public ShoppingCartResponse(string username)
    {
        UserName=username;
    }
    public decimal TotalPrice
    {
        get
        {
            decimal TotalPrice = 0;
            foreach (var item in Items)
            {
                TotalPrice += (item.Price*item.Quantity);
            }
            return TotalPrice;
        }
    }
}
