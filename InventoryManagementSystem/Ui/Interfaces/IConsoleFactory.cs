using Microsoft.VisualBasic;

namespace InventoryManagementSystem.Ui.Interfaces;

/// <summary>
/// Creates instances of console user interfaces
/// </summary>
public interface IConsoleFactory
{
    /// <summary>
    /// Gets a new instance of an <see cref="TInterface"/> 
    /// </summary>
    /// <typeparam name="TInterface"> Type of console </typeparam>
    /// <returns> Instance of new console user interface </returns>
    TInterface GetConsole<TInterface>() where TInterface : class, IConsoleBase;
}