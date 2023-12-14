using InventoryManagementSystem.Interfaces;
using InventoryManagementSystem.Ui.Interfaces;
using PPlus;
using PPlus.Controls;

namespace InventoryManagementSystem.Ui.Consoles;

/// <inheritdoc cref="IRemoveProductConsole"/>
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

    /// <inheritdoc />
    public override void Load()
    {
        base.Load();

        PromptPlus.WriteLine("Ok, let's remove a product!");
        PromptPlus.WriteLine("Here's a list of products, have an eye on the ID number");
        PromptPlus.WriteLine();

        _inventoryManagementService.Products.ForEach(p => PromptPlus.WriteLine($"Id: {p.Id} \t Name: {p.Name} \t Price: {p.Price.ToString("N2")} €"));

        PromptPlus.WriteLine();

        var productIdText = PromptPlus.Input("Which product should be removed?")
            .MaxLength(5)
            .AddValidators(PromptValidators.IsNumber())
            .Run();

        var productId = Convert.ToInt32(productIdText);

        if (!_inventoryManagementService.TryGetProduct(productId, out var product))
        {
            PromptPlus.WriteLine("Product could not be found! Please try again...");
            Restart();
            return;
        }

        if (product == null)
        {
            PromptPlus.WriteLine("Product could not be found! Please try again...");
            Restart();
            return;
        }

        var result = PromptPlus.Confirm("Just to be sure... Is this the product you want to remove?" 
                                        + Environment.NewLine
                                        + $"Id: {product.Id} \t Name: {product.Name} \t Price: {product.Price.ToString("N2")} €").Run();
        
        if (result.IsAborted || result.Value.Key == ConsoleKey.N)
        {
            Restart();
            return;
        }

        if (!_inventoryManagementService.RemoveProduct(productId))
        {
            PromptPlus.WriteLine($"Failed to remove product with id [{productId}]!");
            Restart();
            return;
        }

        ;

        PromptPlus.WriteLine($"Successfully removed product with id [{productId}]");

        ReturnToMainMenu();
    }

    /// <summary>
    /// Restarts this console user interface
    /// </summary>
    private void Restart()
    {
        PromptPlus.WriteLine("Press any key...");
        PromptPlus.ReadKey();
        ConsoleFactory.GetConsole<IRemoveProductConsole>().Load();
    }
}