namespace InventoryManagementSystem.Core.Models;

/// <summary>
/// Model of a product
/// </summary>
public class Product
{
    /// <summary>
    /// Empty Product
    /// </summary>
    public static Product Empty = new()
    {
        Id = -1,
        Name = string.Empty,
        Price = 0
    };

    /// <summary>
    /// Id of product
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Name of product
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Price of product
    /// </summary>
    public float Price { get; set; }
}