using InventoryManagementSystem.Ui.Interfaces;

namespace InventoryManagementSystem.Ui.Models;

public class ConsoleMenuItem
{
    /// <summary>
    /// Console text
    /// </summary>
    public string Text { get; set; } = string.Empty;

    /// <summary>
    /// Set of valid keys
    /// </summary>
    public ConsoleKey[] ValidConsoleKeys { get; set; } = Array.Empty<ConsoleKey>();

    /// <summary>
    /// Function that returns a new console
    /// </summary>
    public Func<IConsoleBase>? Function { get; set; }
}