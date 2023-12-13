using DryIoc;
using InventoryManagementSystem.Interfaces;
using InventoryManagementSystem.Ui.Consoles;
using InventoryManagementSystem.Ui.Interfaces;

namespace InventoryManagementSystem.Ui;

/// <inheritdoc />
public class ConsoleInterface : IUserInterface
{
    private readonly IPersistService _persistService;

    public ConsoleInterface(
        IContainer container,
        IPersistService persistService)
    {
        _persistService = persistService;

        Console.Title = "Inventory Management System";
        Console.BackgroundColor = ConsoleColor.Blue;
        Console.ForegroundColor = ConsoleColor.White;
        Console.CancelKeyPress += ConsoleOn_CancelKeyPress;

        container.Register<IConsoleFactory, ConsoleFactory>(Reuse.Singleton);

        container
            .Resolve<IConsoleFactory>()
            .GetConsole<IMainMenuConsole>()
            .Load();
    }

    private void ConsoleOn_CancelKeyPress(object? sender, ConsoleCancelEventArgs e)
    {
        //TODO Implement global shutdown!
    }
}