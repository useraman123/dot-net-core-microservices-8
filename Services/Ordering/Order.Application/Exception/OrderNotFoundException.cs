namespace Order.Application.Exception;

public class OrderNotFoundException:ApplicationException
{
    public OrderNotFoundException(string name,Object key):base($"Entity {name} - {key} not found")
    {
        
    }
}
