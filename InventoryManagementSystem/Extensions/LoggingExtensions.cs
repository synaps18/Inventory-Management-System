namespace InventoryManagementSystem.Extensions;

/// <summary>
/// Extensino methods to log messages
/// </summary>
public static class LoggingExtensions
{
    /// <summary>
    /// Logs an error message
    /// </summary>
    /// <param name="obj"> Source object of log requesst </param>
    /// <param name="message"> Message to log </param>
    public static void Error(this object obj, string message)
    {
        Console.WriteLine(message);
    }

    /// <summary>
    /// Logs an error message
    /// </summary>
    /// <param name="obj"> Source object of log requesst </param>
    /// <param name="message"> Message to log </param>
    /// <param name="exception"> Exception to log </param>
    public static void Error(this object obj, string message, Exception exception)
    {
        Console.WriteLine(message + Environment.NewLine + exception.Message);
    }

    /// <summary>
    /// Logs an warning message
    /// </summary>
    /// <param name="obj"> Source object of log requesst </param>
    /// <param name="message"> Message to log </param>
    public static void Warn(this object obj, string message)
    {
        Console.WriteLine(message);
    }

    /// <summary>
    /// Logs an info message
    /// </summary>
    /// <param name="obj"> Source object of log requesst </param>
    /// <param name="message"> Message to log </param>
    public static void Info(this object obj, string message)
    {
        Console.WriteLine(message);
    }
}