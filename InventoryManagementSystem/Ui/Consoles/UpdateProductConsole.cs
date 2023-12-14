using InventoryManagementSystem.Interfaces;
using InventoryManagementSystem.Ui.Interfaces;
using PPlus;
using PPlus.Controls;

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

        PromptPlus.WriteLine("Ok, let's update a product!");
        PromptPlus.WriteLine("Here's a list of products, have an eye on the ID number");
        PromptPlus.WriteLine();

        _inventoryManagementService.Products.ForEach(p => PromptPlus.WriteLine($"Id: {p.Id} \t Name: {p.Name} \t Price: {p.Price.ToString("N2")} €"));

        PromptPlus.WriteLine();

        var productIdText = PromptPlus.Input("Which product should be updated?")
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


        //To be sure, ask 
        var resultRealyWantToUpdate = PromptPlus.Confirm("Just to be sure... Is this the product you want to update?"
                                        + Environment.NewLine
                                        + $"Id: {product.Id} \t Name: {product.Name} \t Price: {product.Price.ToString("N2")} €").Run();

        if (resultRealyWantToUpdate.IsAborted)
        {
            Restart();
            return;
        }


        //Ask for Name
        var resultChangeTheName = PromptPlus.Confirm("Want to change the name?").Run();
        if (!resultChangeTheName.IsAborted)
        {
            var newProductName = PromptPlus.Input("Name: ")
                .MaxLength(10)
                .Run();

            if (!newProductName.IsAborted)
            {
                product.Name = newProductName.Value;
            }
        }


        //Ask for Prize
        var resultChangeThePrize = PromptPlus.Confirm("Want to change the prize?").Run();
        if (!resultChangeThePrize.IsAborted)
        {
            var newProductName = PromptPlus.Input("Prize: ")
                .MaxLength(10)
                .AddValidators(PromptValidators.IsTypeFloat())
                .Run();

            if (!newProductName.IsAborted)
            {
                product.Price = Convert.ToSingle(newProductName.Value);
            }
        }

        ReturnToMainMenu();
    }

    /// <summary>
    /// Restarts this console user interface
    /// </summary>
    private void Restart()
    {
        PromptPlus.WriteLine("Press any key...");
        PromptPlus.ReadKey();
        ConsoleFactory.GetConsole<IUpdateProductConsole>().Load();
    }
}