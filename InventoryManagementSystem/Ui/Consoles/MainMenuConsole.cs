 using InventoryManagementSystem.Extensions;
 using InventoryManagementSystem.Ui.Extensions;
 using InventoryManagementSystem.Ui.Interfaces;
using InventoryManagementSystem.Ui.Models;
using PPlus;
 using PPlus.Controls;

 namespace InventoryManagementSystem.Ui.Consoles;

/// <inheritdoc cref="IMainMenuConsole"/>
public class MainMenuConsole : ConsoleBase, IMainMenuConsole
{
    private readonly List<ConsoleMenuItem> _consoleMenuItems = new();

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
        _consoleMenuItems.Clear();

        _consoleMenuItems.Add(new ConsoleMenuItem()
        {
            ValidConsoleKeys = new[] { ConsoleKey.D1 , ConsoleKey.NumPad1 },
            Function = ConsoleFactory.GetConsole<IAddProductConsole>,
            Description = "1: Add a product"
        });

        _consoleMenuItems.Add(new ConsoleMenuItem()
        {
            ValidConsoleKeys = new[] { ConsoleKey.D2, ConsoleKey.NumPad2 },
            Function = ConsoleFactory.GetConsole<IUpdateProductConsole>,
            Description = "2: Update a product"
        });

        _consoleMenuItems.Add(new ConsoleMenuItem()
        {
            ValidConsoleKeys = new[] { ConsoleKey.D3, ConsoleKey.NumPad3 },
            Function = ConsoleFactory.GetConsole<IRemoveProductConsole>,
            Description = "3: Remove a product"
        });

        _consoleMenuItems.Add(new ConsoleMenuItem()
        {
            ValidConsoleKeys = new[] { ConsoleKey.D4, ConsoleKey.NumPad4 },
            Function = ConsoleFactory.GetConsole<IListAllProductsConsole>,
            Description = "4: List all products"
        });

        _consoleMenuItems.Add(new ConsoleMenuItem()
        {
            ValidConsoleKeys = new[] { ConsoleKey.D5, ConsoleKey.NumPad5 },
            Function = ConsoleFactory.GetConsole<ICountOfProductsConsole>,
            Description = "5: Count of all products"
        });

        _consoleMenuItems.Add(new ConsoleMenuItem()
        {
            ValidConsoleKeys = new[] { ConsoleKey.D6, ConsoleKey.NumPad6 },
            Function = ConsoleFactory.GetConsole<IValueOfInventoryConsole>,
            Description = "6: Value of all products"
        });
    }

    /// <inheritdoc />
    public override void Load()
    {
        base.Load();

        PrintMainMenu();
        WaitForKeyAndLoadConsole();
    }

    /// <summary>
    /// Printout the maine menu
    /// </summary>
    private void PrintMainMenu()
    {
        PromptPlus.Banner("ITWH - Sample").Run(bannerDash: BannerDashOptions.DoubleBorderDown);
        PromptPlus.WriteLine("Welcome to the Inventory Management System");
        PromptPlus.WriteLine();
        PromptPlus.WriteLine("What would you like to do?");
        PromptPlus.WriteLine();

        _consoleMenuItems.ForEach(item => PromptPlus.WriteLine(item.Description));
    }

    /// <summary>
    /// Waits for user to enter the key and loads the related console
    /// </summary>
    private void WaitForKeyAndLoadConsole()
    {
        PromptPlus.WriteLine();

        if (!_consoleMenuItems.AskUserForKey(out var validKeyFromUser))
        {
            this.Info("Key input for main menu failed or aborted");
            ReturnToMainMenu();
            return;
        };

        var menuItem = _consoleMenuItems.Find(item => item.ValidConsoleKeys.Any(key => key == validKeyFromUser));
        if (menuItem == null)
        {
            this.Error($"Action for key [{validKeyFromUser}] is null!");
            ReturnToMainMenu();
            return;
        }

        var nextConsole = menuItem.Function();
        nextConsole.Load();
    }
}