using InventoryManagementSystem.Extensions;
using InventoryManagementSystem.Ui.Interfaces;

namespace InventoryManagementSystem.Ui.Consoles;

public abstract class ConsoleBase
{
    protected readonly IConsoleFactory ConsoleFactory;

    protected ConsoleBase(IConsoleFactory consoleFactory)
    {
        ConsoleFactory = consoleFactory;
    }

    public virtual void Load()
    {
        this.Info($"Console [{this.GetType().Name}] loaded");

        Console.Clear();
    }


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

    protected string GetUserInput(string message, string errorMessage, int retry = 2)
    {
        Console.WriteLine(message);
        var userInput = Console.ReadLine();

        while (string.IsNullOrEmpty(userInput))
        {
            if (retry <= 1)
            {
                Console.WriteLine(errorMessage);
                return string.Empty;
            }

            Console.WriteLine(errorMessage);
            userInput = Console.ReadLine();

            retry--;
        }

        return userInput;
    }

    protected void ReturnToMainMenu()
    {
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();

        ConsoleFactory.GetConsole<IMainMenuConsole>().Load();
    }

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