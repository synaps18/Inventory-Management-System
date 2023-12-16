using InventoryManagementSystem.Core.Extensions;
using InventoryManagementSystem.Ui.Interfaces;
using PPlus;
using IConsoleBase = InventoryManagementSystem.Ui.Interfaces.IConsoleBase;

namespace InventoryManagementSystem.Ui.Consoles;

/// <inheritdoc />
public abstract class ConsoleBase : IConsoleBase
{
    protected readonly IConsoleFactory ConsoleFactory;

    protected ConsoleBase(IConsoleFactory consoleFactory)
    {
        ConsoleFactory = consoleFactory;
    }

    /// <inheritdoc />
    public virtual void Load()
    {
        this.Info($"Console [{this.GetType().Name}] loaded");

        PromptPlus.Clear();
    }

    /// <summary>
    /// Try to wait for a specific key input of the user
    /// </summary>
    /// <param name="validKey"> The valid key that was entered </param>
    /// <param name="validKeys"> List of possible valid keys </param>
    /// <param name="retries"> Times to retry waiting in case that the give key is invalid </param>
    /// <returns> User entered a valid key or not </returns>
    protected bool TryWaitForKeyInput(out ConsoleKey validKey, List<ConsoleKey> validKeys, int retries = 5)
    {
        var keyInfo = PromptPlus.ReadKey();

        while (validKeys.All(k => k != keyInfo.Key))
        {
            if (retries <= 1)
            {
                PromptPlus.WriteLine("Retries exceeded... ");
                validKey = default;
                return false;
            }

            PromptPlus.WriteLine("Invalid key! Possible Keys are: " + string.Join(", ", validKeys));
            PromptPlus.WriteLine("Please enter a valid key");

            keyInfo = PromptPlus.ReadKey();
            retries--;
        }

        validKey = keyInfo.Key;
        return true;
    }

    /// <summary>
    /// Returns to the main menu
    /// </summary>
    protected void ReturnToMainMenu()
    {
        PromptPlus.WriteLine("Press any key to continue...");
        PromptPlus.ReadKey();

        ConsoleFactory.GetConsole<IMainMenuConsole>().Load();
    }
}