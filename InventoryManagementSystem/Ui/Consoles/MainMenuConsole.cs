using System.Diagnostics.Tracing;
using InventoryManagementSystem.Extensions;
using InventoryManagementSystem.Ui.Interfaces;
using PPlus;
using IConsoleBase = InventoryManagementSystem.Ui.Interfaces.IConsoleBase;

namespace InventoryManagementSystem.Ui.Consoles;

/// <inheritdoc cref="IMainMenuConsole"/>
public class MainMenuConsole : ConsoleBase, IMainMenuConsole
{
    private readonly Dictionary<ConsoleKey, Func<IConsoleBase>> _mainMenuActions = new();

    public MainMenuConsole(
        IConsoleFactory consoleFactory
    ) : base(consoleFactory)
    {
        InitializeMainMenu();
    }

    /// <summary>
    /// Initializes the main menu
    /// </summary>
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

    /// <summary>
    /// Printout the maine menu and asks user for next action
    /// </summary>
    private void MainMenu()
    {
        PromptPlus.WriteLine("Welcome to the Inventory Management System");
        PromptPlus.WriteLine();
        PromptPlus.WriteLine("What would you like to do?");
        PromptPlus.WriteLine();
        PromptPlus.WriteLine("1: Add a product");
        PromptPlus.WriteLine("2: Update a product");
        PromptPlus.WriteLine("3: Remove a product");
        PromptPlus.WriteLine("4: List all products");
        PromptPlus.WriteLine("5: Count of products");
        PromptPlus.WriteLine("6: Value of inventory");
        PromptPlus.WriteLine();
        PromptPlus.Write("Key: ");

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

        PromptPlus.WriteLine();
        PromptPlus.WriteLine();

        var console = action();

        console.Load();
    }

    /// <inheritdoc />
    public override void Load()
    {
        base.Load();

        MainMenu();
    }
}