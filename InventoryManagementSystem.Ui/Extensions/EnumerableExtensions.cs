using InventoryManagementSystem.Ui.Models;
using PPlus;

namespace InventoryManagementSystem.Ui.Extensions;

/// <summary>
/// Ui related extension methods for type <see cref="IEnumerable{T}"/>
/// </summary>
public static class EnumerableExtensions
{
    /// <summary>
    /// Asks user for keys used in <paramref name="menuITems"/>
    /// </summary>
    /// <param name="menuITems"> List of <see cref="ConsoleMenuItem"/>s </param>
    /// <param name="keyInfo"> Entered key </param>
    /// <returns> User input was successfull or not </returns>
    public static bool AskUserForKey(this IEnumerable<ConsoleMenuItem> menuITems, out ConsoleKey keyInfo)
    {
        var possibleMainMenuKeys = menuITems.SelectMany(a => a.ValidConsoleKeys).ToList();

        var promptPlusKeyPress = PromptPlus.KeyPress("Key: ");

        possibleMainMenuKeys.ForEach(k => promptPlusKeyPress.AddKeyValid(k));

        var pressedKey = promptPlusKeyPress.Run();

        if (pressedKey.IsAborted)
        {
            keyInfo = default;
            return false;
        }

        keyInfo = pressedKey.Value.Key;
        return true;
    }
}