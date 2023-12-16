using InventoryManagementSystem.Core.Interfaces;
using InventoryManagementSystem.Ui.Interfaces;
using PPlus;

namespace InventoryManagementSystem.Ui.Consoles;

/// <inheritdoc cref="ICountOfProductsConsole"/>
public class CountOfProductsConsole : ConsoleBase, ICountOfProductsConsole
{
    private readonly IInventoryManagementService _inventoryManagementService;

    public CountOfProductsConsole(
        IConsoleFactory consoleFactory,
        IInventoryManagementService inventoryManagementService
    ) : base(consoleFactory)
    {
        _inventoryManagementService = inventoryManagementService;
    }

    /// <inheritdoc />
    public override void Load()
    {
        base.Load();

        PromptPlus.WriteLine($"Count of products: {_inventoryManagementService.Count}");

        ReturnToMainMenu();
    }
}