using DryIoc;
using InventoryManagementSystem.Core.Interfaces;
using InventoryManagementSystem.Ui.Interfaces;
using PPlus;

namespace InventoryManagementSystem.Ui;

/// <inheritdoc />
public class ConsoleInterface : IUserInterface
{
    private readonly IConsoleFactory _consoleFactory;

    public ConsoleInterface(
        IContainer container)
    {
        Console.Title = "Inventory Management System";
        Console.CancelKeyPress += ConsoleOn_CancelKeyPress;

        PromptPlus.BackgroundColor = ConsoleColor.Blue;
        PromptPlus.ForegroundColor = ConsoleColor.White;
        

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