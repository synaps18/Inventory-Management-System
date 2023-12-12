using InventoryManagementSystem.Interfaces;
using InventoryManagementSystem.Ui.Interfaces;

namespace InventoryManagementSystem.Ui.Consoles;

public class RemoveProductConsole : ConsoleBase, IRemoveProductConsole
{
    private readonly IInventoryManagementService _inventoryManagementService;

    public RemoveProductConsole(
        IConsoleFactory consoleFactory,
        IInventoryManagementService inventoryManagementService
        ) : base(consoleFactory)
    {
        _inventoryManagementService = inventoryManagementService;
    }

    public override void Load()
    {
        base.Load();

        Console.WriteLine("Nothing here yet...");
       
        ReturnToMainMenu();
    }
}