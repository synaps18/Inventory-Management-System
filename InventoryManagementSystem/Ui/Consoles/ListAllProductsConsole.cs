using InventoryManagementSystem.Interfaces;
using InventoryManagementSystem.Models;
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

        PromptPlus.Table<Product>("All Products")
            .AddItems(_inventoryManagementService.Products)
            .AutoFill(30, 80)
            .AddFormatType<float>(a => ((float)a).ToString(("N2")) + " €")
            .Run();

        ReturnToMainMenu();
    }
}