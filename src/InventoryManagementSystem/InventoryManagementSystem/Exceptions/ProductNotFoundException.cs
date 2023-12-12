namespace InventoryManagementSystem.Exceptions;

public class ProductNotFoundException : Exception
{
    public int RequestedId { get; set; }

    public ProductNotFoundException(int requestedId)
    {
        RequestedId = requestedId;
    }
}