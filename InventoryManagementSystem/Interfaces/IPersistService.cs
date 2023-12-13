using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Interfaces;

/// <summary>
/// Persistand handler to persist inventory
/// </summary>
public interface IPersistService
{
    /// <summary>
    /// Persists the inventory of products
    /// </summary>
    /// <param name="inventory"> Inventory to persist </param>
    void Persist(List<Product> inventory);

    /// <summary>
    /// Restores the inventory of products
    /// </summary>
    /// <returns></returns>
    List<Product> Restore();
}