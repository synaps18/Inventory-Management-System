namespace InventoryManagementSystem.Exceptions;

/// <summary>
///     Product was not found exception
/// </summary>
public class ProductNotFoundException : Exception
{
    public ProductNotFoundException(int requestedId)
    {
        RequestedId = requestedId;
    }

    /// <summary>
    ///     Requested id that cannot be found
    /// </summary>
    public int RequestedId { get; set; }
}