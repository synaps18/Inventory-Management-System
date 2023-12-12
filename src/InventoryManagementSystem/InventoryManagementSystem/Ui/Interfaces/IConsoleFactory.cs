using Microsoft.VisualBasic;

namespace InventoryManagementSystem.Ui.Interfaces;

public interface IConsoleFactory
{
    TInterface GetConsole<TInterface>() where TInterface : class;
}