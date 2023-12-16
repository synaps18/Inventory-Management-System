using System.Collections.Immutable;
using InventoryManagementSystem.Core.Models;

namespace InventoryManagementSystem.Core.Interfaces;

/// <summary>
/// Inventory management system that manages vrious products
/// </summary>
public interface IInventoryManagementService
{
    /// <summary>
    /// Adds a product
    /// </summary>
    bool AddProduct(string name, float price);

    /// <summary>
    /// Removes a product
    /// </summary>
    bool RemoveProduct(int id);

    /// <summary>
    /// Updates a product
    /// </summary>
    void UpdateProduct(int id, string name, float price);

    /// <summary>
    /// Try getting a product. 
    /// </summary>
    /// <param name="id"> Id of the product </param>
    /// <param name="product"> The product, if found</param>
    /// <returns> If product exists returns true, otherwise false </returns>
    bool TryGetProduct(int id, out Product? product);

    /// <summary>
    /// Count of current products in inventory
    /// </summary>
    public int Count { get; }

    /// <summary>
    /// Value of whole inventory
    /// </summary>
    public float ValueOfInventory { get; }

    /// <summary>
    /// All products
    /// </summary>
    public ImmutableList<Product> Products { get; }
}