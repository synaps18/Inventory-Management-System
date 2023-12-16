using System.Text.Json;
using InventoryManagementSystem.Core.Extensions;
using InventoryManagementSystem.Core.Interfaces;
using InventoryManagementSystem.Core.Models;

namespace InventoryManagementSystem.Services;

/// <inheritdoc />
public class PersistService : IPersistService
{
    private const string Filename = "products.json";

    /// <inheritdoc />
    public void Persist(List<Product> inventory)
    {
        var serializedProducts = JsonSerializer.Serialize(inventory);
        File.WriteAllText(Filename, serializedProducts);
    }

    /// <inheritdoc />
    public List<Product> Restore()
    {
        try
        {
            if (!File.Exists(Filename))
                return new List<Product>();

            var serializedData = File.ReadAllText(Filename);
            var products = JsonSerializer.Deserialize<List<Product>>(serializedData);
            return products ?? new List<Product>();
        }
        catch (Exception e)
        {
            this.Error("Failed to restore products", e);
            return new List<Product>();
        }
    }
}