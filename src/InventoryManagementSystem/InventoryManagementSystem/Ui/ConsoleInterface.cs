using System.Globalization;
using InventoryManagementSystem.Extensions;
using InventoryManagementSystem.Interfaces;

namespace InventoryManagementSystem.Ui;

public class ConsoleInterface : IUserInterface
{
    private readonly IInventoryManagementService _inventoryManagementService;
    private readonly Dictionary<ConsoleKey, Action> _mainMenuActions = new();

    public ConsoleInterface(IInventoryManagementService inventoryManagementService)
    {
        _inventoryManagementService = inventoryManagementService;

        InitializeMainMenu();
        MainMenu();
    }

    private void InitializeMainMenu()
    {
        _mainMenuActions.Clear();

        _mainMenuActions.Add(ConsoleKey.D1, AddProduct);
        _mainMenuActions.Add(ConsoleKey.NumPad1, AddProduct);
        _mainMenuActions.Add(ConsoleKey.D2, UpdateProduct);
        _mainMenuActions.Add(ConsoleKey.NumPad2, UpdateProduct);
        _mainMenuActions.Add(ConsoleKey.D3, RemoveProduct);
        _mainMenuActions.Add(ConsoleKey.NumPad3, RemoveProduct);
        _mainMenuActions.Add(ConsoleKey.D4, CountOfProducts);
        _mainMenuActions.Add(ConsoleKey.NumPad4, CountOfProducts);
        _mainMenuActions.Add(ConsoleKey.D5, ValueOfInventory);
        _mainMenuActions.Add(ConsoleKey.NumPad5, ValueOfInventory);
    }

    private void ValueOfInventory()
    {
        Console.WriteLine($"Value of inventory: {_inventoryManagementService.ValueOfInventory}");
        ReturnToMainMenu();
    }

    private void CountOfProducts()
    {
        Console.WriteLine($"Count of products: {_inventoryManagementService.Count}");
        ReturnToMainMenu();
    }

    private void RemoveProduct()
    {
        Console.WriteLine("Nothing here yet...");
        ReturnToMainMenu();
    }

    private void UpdateProduct()
    {
        Console.WriteLine("Nothing here yet...");
        ReturnToMainMenu();
    }

    private void AddProduct()
    {
        Console.Clear();
        Console.WriteLine("Ok! Let's add a new product");
        
        Console.WriteLine();
        var productName = GetUserInput("Choose a name:", "Entered name was empty! Please choose a name:", 2);

        Console.WriteLine();
        var productPriceText = string.Empty;
        var productPrice = 0f;

        do
        {
            productPriceText = GetUserInput("How much is the fish?", "Entered price was empty! Please give me a value:", 2);
        } while (!float.TryParse(productPriceText, NumberStyles.Any, CultureInfo.InvariantCulture, out productPrice));
        
        var productAdded = _inventoryManagementService.AddProduct(productName!, productPrice);

        var userInfo = productAdded ? "Alright! Product added" : "Adding the product was not successfull";
        Console.WriteLine(userInfo);

        ReturnToMainMenu();
    }

    private void ReturnToMainMenu(bool clearConsole = true)
    {
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();

        MainMenu(clearConsole);
    }

    private string GetUserInput(string message, string errorMessage, int retry = 2)
    {
        Console.WriteLine(message);
        var userInput = Console.ReadLine();

        while (string.IsNullOrEmpty(userInput))
        {
            if (retry <= 1)
            {
                Console.WriteLine(errorMessage);
                return string.Empty;
            }

            Console.WriteLine(errorMessage);
            userInput = Console.ReadLine();

            retry--;
        }

        return userInput;
    }

    private void MainMenu(bool clearConsole = true)
    {
        if(clearConsole) Console.Clear();
        Console.WriteLine("Welcome to the Inventory Management System");
        Console.WriteLine();
        Console.WriteLine("What would you like to do?");
        Console.WriteLine();
        Console.WriteLine("1: Add a product");
        Console.WriteLine("2: Update a product");
        Console.WriteLine("3: Remove a product");
        Console.WriteLine("4: Count of products");
        Console.WriteLine("5: Value of inventory");
        Console.WriteLine();
        Console.Write("Key: ");

        var possibleMainMenuKeys = _mainMenuActions.Select(a => a.Key).ToList();
        if (!TryWaitForKeyInput(out var validKeyFromUser, possibleMainMenuKeys))
        {
            this.Warn("User entered invalid number for main menu");

            ReturnToMainMenu();
            return;
        }

        if (!_mainMenuActions.TryGetValue(validKeyFromUser, out var action))
        {
            this.Error($"Key [{validKeyFromUser}] not found!");
        }

        if (action == null)
        {
            this.Error($"Action for key [{validKeyFromUser}] is null!");
            return;
        }

        Console.WriteLine();
        Console.WriteLine();
        action();
    }

    private bool TryWaitForKeyInput(out ConsoleKey validKey, List<ConsoleKey> validKeys, int retries = 5)
    {
        var keyInfo = Console.ReadKey();

        while (validKeys.All(k => k != keyInfo.Key))
        {
            if (retries <= 1)
            {
                Console.WriteLine("Retries exceeded... ");
                validKey = default;
                return false;
            }

            Console.WriteLine("Invalid key! Possible Keys are: " + string.Join(", ", validKeys));
            Console.WriteLine("Please enter a valid key");

            keyInfo = Console.ReadKey();
            retries--;
        }

        validKey = keyInfo.Key;
        return true;
    }
}