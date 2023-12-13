using InventoryManagementSystem.Extensions;
using InventoryManagementSystem.Ui.Interfaces;

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

        Console.Clear();
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
        var keyInfo = Console.ReadKey();

        while (validKeys.All(k => k != keyInfo.Key))
        {
            if (retries <= 1)
            {
                Console.WriteLine("Retries exceeded... ");
                validKey = default;
                return false;
            }

            Console.WriteLine("Invalid key! Possible Keys are: " + string.Join(", ", validKeys));
            Console.WriteLine("Please enter a valid key");

            keyInfo = Console.ReadKey();
            retries--;
        }

        validKey = keyInfo.Key;
        return true;
    }

    /// <summary>
    /// Gets text input of the user
    /// </summary>
    /// <param name="message"> Request message for user </param>
    /// <param name="errorMessage"> Error message if no retries left </param>
    /// <param name="retry"> Times to retry the request </param>
    /// <returns> Given user input </returns>
    protected string GetUserInput(string message, string errorMessage, int retry = 2)
    {
        Console.WriteLine(message);
        var userInput = Console.ReadLine();

        while (string.IsNullOrEmpty(userInput))
        {
            Console.WriteLine(errorMessage);
            
            if (retry <= 1)
            {
                return string.Empty;
            }

            userInput = Console.ReadLine();

            retry--;
        }

        return userInput;
    }

    /// <summary>
    /// Returns to the main menu
    /// </summary>
    protected void ReturnToMainMenu()
    {
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();

        ConsoleFactory.GetConsole<IMainMenuConsole>().Load();
    }

    /// <summary>
    /// Gets yes or no (y / n) from user and validates it
    /// </summary>
    /// <returns> Yes: <b>true</b> No or invalid: <b>false</b></returns>
    protected bool UserInputYesOrNo()
    {
        if (!TryWaitForKeyInput(out var enteredKey, new List<ConsoleKey>()
            {
                ConsoleKey.Y,
                ConsoleKey.N
            }))
        {
            Console.WriteLine("Tried too many times... Leaving");
            return false;
        };

        if (enteredKey == ConsoleKey.N)
        {
            Console.WriteLine("Ok!");
            return false;
        }

        if (enteredKey != ConsoleKey.Y)
        {
            Console.WriteLine("Entered invalid key. Leaving...");
            return false;
        }

        return true;
    }
}