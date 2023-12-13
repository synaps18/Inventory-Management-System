using DryIoc;
using InventoryManagementSystem.Interfaces;
using InventoryManagementSystem.Ui.Consoles;
using InventoryManagementSystem.Ui.Interfaces;

namespace InventoryManagementSystem.Ui;

/// <inheritdoc />
public class ConsoleInterface : IUserInterface
{
    private readonly IConsoleFactory _consoleFactory;

    public ConsoleInterface(
        IContainer container)
    {
        Console.Title = "Inventory Management System";
        Console.BackgroundColor = ConsoleColor.Blue;
        Console.ForegroundColor = ConsoleColor.White;
        Console.CancelKeyPress += ConsoleOn_CancelKeyPress;

        container.Register<IConsoleFactory, ConsoleFactory>(Reuse.Singleton);
        _consoleFactory = container.Resolve<IConsoleFactory>();

        LoadMainMenu();
    }

    private void LoadMainMenu()
    {
        _consoleFactory.GetConsole<IMainMenuConsole>().Load();
    }

    private void ConsoleOn_CancelKeyPress(object? sender, ConsoleCancelEventArgs e)
    {
       e.Cancel = true;
    }
}