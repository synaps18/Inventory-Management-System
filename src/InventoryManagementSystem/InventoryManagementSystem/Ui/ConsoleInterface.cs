using DryIoc;
using InventoryManagementSystem.Interfaces;
using InventoryManagementSystem.Ui.Consoles;
using InventoryManagementSystem.Ui.Interfaces;

namespace InventoryManagementSystem.Ui;

public class ConsoleInterface : IUserInterface
{
    public ConsoleInterface(IContainer container)
    {
        Console.Title = "Inventory Management System";
        Console.BackgroundColor = ConsoleColor.Blue;
        Console.ForegroundColor = ConsoleColor.White;
        
        container.Register<IConsoleFactory, ConsoleFactory>(Reuse.Singleton);

        container
            .Resolve<IConsoleFactory>()
            .GetConsole<IMainMenuConsole>()
            .Load();
    }
}