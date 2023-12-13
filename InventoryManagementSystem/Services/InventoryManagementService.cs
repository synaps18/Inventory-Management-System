using System.Collections.Immutable;
using InventoryManagementSystem.Exceptions;
using InventoryManagementSystem.Interfaces;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Services;

///<inheritdoc />
public class InventoryManagementService : IInventoryManagementService
{
    private readonly HashSet<Product> _products = new();
    private readonly IPersistService _persistService;

    public InventoryManagementService(
        IPersistService persistService)
    {
        _persistService = persistService;

        AppDomain.CurrentDomain.ProcessExit += CurrentDomain_OnProcessExit;

        Restore();
    }

    ///<inheritdoc />
    public float ValueOfInventory => _products.Sum(p => p.Price);

    ///<inheritdoc />
    public ImmutableList<Product> Products => _products.ToImmutableList();

    ///<inheritdoc />
    public int Count => _products.Count;

    ///<inheritdoc />
    public bool AddProduct(string name, float price)
    {
        var productAdded = _products.Add(new Product
        {
            Id = _products.Count > 0 ? _products.Max(a => a.Id) + 1 : 0,
            Name = name,
            Price = price
        });

        return productAdded;
    }

    ///<inheritdoc />
    public bool RemoveProduct(int id)
    {
        var productRemoved = _products.RemoveWhere(p => p.Id == id);
        return productRemoved > 0;
    }

    ///<inheritdoc />
    public bool TryGetProduct(int id, out Product? product)
    {
        var productFromInventory = _products.FirstOrDefault(p => p.Id == id);
        if (productFromInventory != null)
        {
            product = productFromInventory;
            return true;
        }

        product = Product.Empty;
        return false;
    }

    /// <inheritdoc />
    /// <exception cref="ProductNotFoundException"> Throws if the product was not found </exception>
    public void UpdateProduct(int id, string name, float price)
    {
        var productFromInventory = _products.FirstOrDefault(p => p.Id == id);
        if (productFromInventory == null) throw new ProductNotFoundException(id);
    }

    /// <summary>
    ///     Persist all products on application exit
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void CurrentDomain_OnProcessExit(object? sender, EventArgs e)
    {
        Persist();
    }

    private void Persist()
    {
        _persistService.Persist(_products.ToList());
    }

    /// <summary>
    ///     Restores all products persisted by <see cref="IPersistService" />
    /// </summary>
    private void Restore()
    {
        var restoredProducts = _persistService.Restore();
        restoredProducts.ForEach(p => _products.Add(p));
    }
}