using InventoryManagementSystem.Interfaces;
using InventoryManagementSystem.Ui.Interfaces;
using PPlus;
using PPlus.Controls;

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

        PromptPlus.WriteLine("Ok! Let's add a new product");
        PromptPlus.WriteLine();

        if (!AskForName(out var productName))
        {
            ReturnToMainMenu();
            return;
        }

        if (!AskForPrice(out var productPrice))
        {
            ReturnToMainMenu();
            return;
        }

        var productAdded = _inventoryManagementService.AddProduct(productName!, productPrice);

        var userInfo = productAdded ? "Alright! Product added" : "Adding the product was not successfull";
        PromptPlus.WriteLine(userInfo);

        ReturnToMainMenu();
    }

    /// <summary>
    /// Asks the user for a name
    /// </summary>
    /// <param name="name"> Entered name </param>
    /// <returns> User input was successfull or not </returns>
    private bool AskForName(out string name)
    {
        //Ask for Name
        var productNameText = PromptPlus.Input("Choose a name")
            .MaxLength(10)
            .DefaultIfEmpty("No name")
            .Run();

        if (productNameText.IsAborted)
        {
            name = string.Empty;
            return false;
        }

        name = productNameText.Value;
        return true;
    }

    /// <summary>
    /// Asks the user for a price
    /// </summary>
    /// <param name="prize"> The entered price </param>
    /// <returns> User input was successfull or not </returns>
    private bool AskForPrice(out float prize)
    {
        //Ask for Price
        var productPrizeText = PromptPlus.Input("How much is the fish?")
            .MaxLength(10)
            .AddValidators(PromptValidators.IsTypeFloat())
            .Run();

        if (productPrizeText.IsAborted)
        {
            prize = 0.0f;
            return false;
        }

        prize = Convert.ToSingle(productPrizeText.Value);
        return true;
    }
}