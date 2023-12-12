using DryIoc;
using InventoryManagementSystem.Ui.Consoles;
using InventoryManagementSystem.Ui.Interfaces;

namespace InventoryManagementSystem.Ui;

public class ConsoleFactory : IConsoleFactory
{
    private readonly IContainer _container;

    public ConsoleFactory(IContainer container)
    {
        _container = container;

        container.Register<IMainMenuConsole, MainMenuConsole>();
        container.Register<IAddProductConsole, AddProductConsole>();
        container.Register<IUpdateProductConsole, UpdateProductConsole>();
        container.Register<IRemoveProductConsole, RemoveProductConsole>();
        container.Register<ICountOfProductsConsole, CountOfProductsConsole>();
        container.Register<IValueOfInventoryConsole, ValueOfInventoryConsole>();
    }

    public TInterface GetConsole<TInterface>() where TInterface : class
    {
        var console = _container.Resolve<TInterface>();
        return console;
    }
}