using InventoryManagementSystem.Interfaces;
using InventoryManagementSystem.Ui.Interfaces;

namespace InventoryManagementSystem.Ui.Consoles;

public class UpdateProductConsole : ConsoleBase, IUpdateProductConsole
{
    private readonly IInventoryManagementService _inventoryManagementService;

    public UpdateProductConsole(
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