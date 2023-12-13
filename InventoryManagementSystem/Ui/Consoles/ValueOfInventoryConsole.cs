using DryIoc.ImTools;
using InventoryManagementSystem.Interfaces;
using InventoryManagementSystem.Ui.Interfaces;

namespace InventoryManagementSystem.Ui.Consoles;

/// <inheritdoc cref="IValueOfInventoryConsole"/>
public class ValueOfInventoryConsole : ConsoleBase, IValueOfInventoryConsole
{
    private readonly IInventoryManagementService _inventoryManagementService;

    public ValueOfInventoryConsole(
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

        Console.WriteLine($"Value of inventory: {_inventoryManagementService.ValueOfInventory:N2} €");

        ReturnToMainMenu();
    }
}