using System.Diagnostics.Tracing;
using InventoryManagementSystem.Extensions;
using InventoryManagementSystem.Ui.Interfaces;

namespace InventoryManagementSystem.Ui.Consoles;

public class MainMenuConsole : ConsoleBase, IMainMenuConsole
{
    private readonly Dictionary<ConsoleKey, Func<IConsoleBase>> _mainMenuActions = new();

    public MainMenuConsole(
        IConsoleFactory consoleFactory
        ) : base(consoleFactory)
    {
        InitializeMainMenu();
    }

    private void InitializeMainMenu()
    {
        _mainMenuActions.Clear();

        _mainMenuActions.Add(ConsoleKey.D1, ConsoleFactory.GetConsole<IAddProductConsole>);
        _mainMenuActions.Add(ConsoleKey.NumPad1, ConsoleFactory.GetConsole<IAddProductConsole>);
        _mainMenuActions.Add(ConsoleKey.D2, ConsoleFactory.GetConsole<IUpdateProductConsole>);
        _mainMenuActions.Add(ConsoleKey.NumPad2, ConsoleFactory.GetConsole<IUpdateProductConsole>);
        _mainMenuActions.Add(ConsoleKey.D3, ConsoleFactory.GetConsole<IRemoveProductConsole>);
        _mainMenuActions.Add(ConsoleKey.NumPad3, ConsoleFactory.GetConsole<IRemoveProductConsole>);
        _mainMenuActions.Add(ConsoleKey.D4, ConsoleFactory.GetConsole<IListAllProductsConsole>);
        _mainMenuActions.Add(ConsoleKey.NumPad4, ConsoleFactory.GetConsole<IListAllProductsConsole>);
        _mainMenuActions.Add(ConsoleKey.D5, ConsoleFactory.GetConsole<ICountOfProductsConsole>);
        _mainMenuActions.Add(ConsoleKey.NumPad5, ConsoleFactory.GetConsole<ICountOfProductsConsole>);
        _mainMenuActions.Add(ConsoleKey.D6, ConsoleFactory.GetConsole<IValueOfInventoryConsole>);
        _mainMenuActions.Add(ConsoleKey.NumPad6, ConsoleFactory.GetConsole<IValueOfInventoryConsole>);
    }

    private void MainMenu()
    {
        Console.WriteLine("Welcome to the Inventory Management System");
        Console.WriteLine();
        Console.WriteLine("What would you like to do?");
        Console.WriteLine();
        Console.WriteLine("1: Add a product");
        Console.WriteLine("2: Update a product");
        Console.WriteLine("3: Remove a product");
        Console.WriteLine("4: List all products");
        Console.WriteLine("5: Count of products");
        Console.WriteLine("6: Value of inventory");
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

        var console = action();

        console.Load();
    }

    public override void Load()
    {
        base.Load();

        MainMenu();
    }
}