using InventoryManagementSystem.Interfaces;
using InventoryManagementSystem.Ui.Interfaces;
using PPlus;

namespace InventoryManagementSystem.Ui.Consoles;

/// <inheritdoc cref="IListAllProductsConsole"/>
public class ListAllProductsConsole : ConsoleBase, IListAllProductsConsole
{
    private readonly IInventoryManagementService _inventoryManagementService;

    public ListAllProductsConsole(
        IConsoleFactory consoleFactory,
        IInventoryManagementService inventoryManagementService) : base(consoleFactory)
    {
        _inventoryManagementService = inventoryManagementService;
    }

    /// <inheritdoc />
    public override void Load()
    {
        base.Load();
        _inventoryManagementService.Products.ForEach(p => PromptPlus.WriteLine($"Id: {p.Id} \t Name: {p.Name} \t Price: {p.Price.ToString("N2")} €"));

        ReturnToMainMenu();
    }
}