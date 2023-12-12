using InventoryManagementSystem.Interfaces;
using InventoryManagementSystem.Ui.Interfaces;

namespace InventoryManagementSystem.Ui.Consoles;

public class ListAllProductsConsole : ConsoleBase, IListAllProductsConsole
{
    private readonly IInventoryManagementService _inventoryManagementService;

    public ListAllProductsConsole(
        IConsoleFactory consoleFactory,
        IInventoryManagementService inventoryManagementService) : base(consoleFactory)
    {
        _inventoryManagementService = inventoryManagementService;
    }

    public override void Load()
    {
        base.Load();
        _inventoryManagementService.Products.ForEach(p => Console.WriteLine($"Id: {p.Id} \t Name: {p.Name} \t Price: {p.Price.ToString("N2")} €"));
        
        ReturnToMainMenu();
    }

}