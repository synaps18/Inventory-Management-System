using InventoryManagementSystem.Interfaces;
using InventoryManagementSystem.Models;
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

        PrintAllProducts();

        PromptPlus.WriteLine();
        PromptPlus.WriteLine();

        if (!AskForProductToUpdate(out var productId))
        {
            ReturnToMainMenu();
            return;
        }

        if (!GetProduct(productId, out var product))
        {
            Restart();
            return;
        }

        if (!EnsureThatTheProductIsChosenOne(product!))
        {
            Restart();
            return;
        }

        if (AskForName(out var productName))
        {
            product!.Name = productName;
        }

        if(AskForPrize(out var price))
        {
            product!.Price = price;
        }

        ReturnToMainMenu();
    }

    private void PrintAllProducts()
    {
        PromptPlus.WriteLine("Ok, let's update a product!");

        PromptPlus.Table<Product>("Here's a list of products, have an eye on the ID number")
            .AddItems(_inventoryManagementService.Products)
            .AutoFill(30, 80)
            .AddFormatType<float>(a => ((float)a).ToString(("N2")) + " €")
            .Run();
    }

    /// <summary>
    /// Asks user for id of the product that should be changed
    /// </summary>
    /// <param name="id"> Id of product </param>
    /// <returns> User input successfull or not </returns>
    private bool AskForProductToUpdate(out int id)
    {
        var productIdText = PromptPlus.Input("Which product should be updated?")
            .MaxLength(5)
            .AddValidators(PromptValidators.IsNumber())
            .Run();

        if (productIdText.IsAborted)
        {
            id = 0;
            return false; 
        }
        
        id = Convert.ToInt32(productIdText.Value);
        return true;
    }

    /// <summary>
    /// Get's the real product from inventory service
    /// </summary>
    /// <param name="id"> Id of product </param>
    /// <param name="product"> The product </param>
    /// <returns> Process successfull </returns>
    private bool GetProduct(int id, out Product? product)
    {
        if (!_inventoryManagementService.TryGetProduct(id, out var productFromManagement))
        {
            PromptPlus.WriteLine("Product not found!");
            product = null;
            return false;
        }

        if (productFromManagement == null)
        {
            PromptPlus.WriteLine("Product is null");
            product = null;
            return false;
        }

        product = productFromManagement;
        return true;
    }

    /// <summary>
    /// Ensures that the chosen product is the correct one
    /// </summary>
    /// <param name="product"> Product that should be changed</param>
    /// <returns> User input was successfull or not </returns>
    private bool EnsureThatTheProductIsChosenOne(Product product)
    {
        //To be sure, ask 
        var resultRealyWantToUpdate = PromptPlus.Confirm("Just to be sure... Is this the product you want to update?"
                                                         + Environment.NewLine
                                                         + $"Id: {product.Id} \t Name: {product.Name} \t Price: {product.Price.ToString("N2")} €").Run();

        return !(resultRealyWantToUpdate.IsAborted || resultRealyWantToUpdate.Value.Key == ConsoleKey.N);     
    }

    /// <summary>
    /// Asks for a new name
    /// </summary>
    /// <param name="name"> New name </param>
    /// <returns> User input was successfull or not </returns>
    private bool AskForName(out string name)
    {
        var resultChangeTheName = PromptPlus.Confirm("Want to change the name?").Run();
        if (resultChangeTheName.IsAborted || resultChangeTheName.Value.Key == ConsoleKey.N)
        {
            name = string.Empty;
            return false;
        }

        var newProductName = PromptPlus.Input("Name: ")
            .MaxLength(10)
            .DefaultIfEmpty("No name")
            .Run();

        if (newProductName.IsAborted)
        {
            name = string.Empty;
            return false;
        }

        name = newProductName.Value;
        return true;
    }

    /// <summary>
    /// Asks user for the price
    /// </summary>
    /// <param name="price"> The new price </param>
    /// <returns> User input was successfull or not </returns>
    private bool AskForPrize(out float price)
    {
        var resultChangeThePrize = PromptPlus.Confirm("Want to change the prize?").Run();
        if (resultChangeThePrize.IsAborted || resultChangeThePrize.Value.Key == ConsoleKey.N)
        {
            price = 0.0f;
            return false;
        }

        var newProductName = PromptPlus.Input("Prize: ")
            .MaxLength(10)
            .AddValidators(PromptValidators.IsTypeFloat())
            .Run();

        if (newProductName.IsAborted)
        {
            price = 0.0f;
            return false;
        }

        price = Convert.ToSingle(newProductName.Value);
        return true;
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