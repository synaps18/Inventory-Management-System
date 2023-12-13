using InventoryManagementSystem.Interfaces;
using InventoryManagementSystem.Ui.Interfaces;

namespace InventoryManagementSystem.Ui.Consoles;

/// <inheritdoc cref="IUpdateProductConsole"/>
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

    /// <inheritdoc />
    public override void Load()
    {
        base.Load();

        Console.WriteLine("Ok, let's update a product!");
        Console.WriteLine("Here's a list of products, have an eye on the ID number");
        Console.WriteLine();

        _inventoryManagementService.Products.ForEach(p => Console.WriteLine($"Id: {p.Id} \t Name: {p.Name} \t Price: {p.Price.ToString("N2")} €"));

        Console.WriteLine();

        var idText = string.Empty;
        var productId = -1;

        var calledMoreThaOnce = false;
        do
        {
            if (calledMoreThaOnce)
                Console.WriteLine("Invalid Format! Please use only numbers without any special characters!");
            calledMoreThaOnce = true;

            idText = GetUserInput("Which product should be updated?", "ID was empty");
        } while (!int.TryParse(idText, out productId));


        if(!_inventoryManagementService.TryGetProduct(productId, out var product))
        {
            Console.WriteLine("Product could not be found! Please try again...");
            Restart();
            return;
        }

        if (product == null)
        {
            Console.WriteLine("Product could not be found! Please try again...");
            Restart();
            return;
        }

        Console.WriteLine("Just to be sure... Is this the product you want to update? (y / n)");
        Console.WriteLine($"Id: {product.Id} \t Name: {product.Name} \t Price: {product.Price.ToString("N2")} €");

        var changeTheProduct = UserInputYesOrNo();
        if (!changeTheProduct)
        {
            Restart();
            return;
        }

        var changeTheName = UserInputYesOrNo();
        if (!changeTheName)
        {
            Restart();
            return;
        }

        //TODO HIER ABFRAGEN WIE DER NAME SEIN SOLL

        var changeThePrize = UserInputYesOrNo();
        if (!changeThePrize)
        {
            Restart();
            return;
        }

        //TODO HIER ABFRAGEN WIE DER PREIS SEIN SOLL

        ReturnToMainMenu();
    }

    /// <summary>
    /// Restarts this console user interface
    /// </summary>
    private void Restart()
    {
        Console.WriteLine("Press any key...");
        Console.ReadKey();
        ConsoleFactory.GetConsole<IUpdateProductConsole>().Load();
    }
}