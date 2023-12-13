using System.Globalization;
using InventoryManagementSystem.Interfaces;
using InventoryManagementSystem.Ui.Interfaces;

namespace InventoryManagementSystem.Ui.Consoles;

/// <inheritdoc cref="IAddProductConsole"/>
public class AddProductConsole : ConsoleBase, IAddProductConsole
{
    private readonly IInventoryManagementService _inventoryManagementService;

    public AddProductConsole(
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

        Console.WriteLine("Ok! Let's add a new product");

        Console.WriteLine();
        var productName = GetUserInput("Choose a name:", "Entered name was empty! Please choose a name:", 2);

        Console.WriteLine();

        var productPriceText = string.Empty;
        var productPrice = 0f;

        var calledMoreThaOnce = false;
        do
        {
            if (calledMoreThaOnce)
                Console.WriteLine("Invalid Format! Please use only numbers in format like [3.42] without currency symbols!");
            calledMoreThaOnce = true;

            productPriceText = GetUserInput("How much is the fish?", "Entered price was empty! Please give me a value:", 2);
        } while (!float.TryParse(productPriceText, NumberStyles.Any, CultureInfo.InvariantCulture, out productPrice));

        var productAdded = _inventoryManagementService.AddProduct(productName!, productPrice);

        var userInfo = productAdded ? "Alright! Product added" : "Adding the product was not successfull";
        Console.WriteLine(userInfo);

        ReturnToMainMenu();
    }
}