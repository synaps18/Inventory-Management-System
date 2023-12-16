using InventoryManagementSystem.Ui.Interfaces;

namespace InventoryManagementSystem.Ui.Models;

public class ConsoleMenuItem
{
    public string Description { get; set; }

    public ConsoleKey[] ValidConsoleKeys { get; set; }

    public Func<IConsoleBase> Function { get; set; }
}