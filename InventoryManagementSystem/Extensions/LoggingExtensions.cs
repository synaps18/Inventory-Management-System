using System.Runtime.CompilerServices;
using PPlus;
using Serilog;
using Serilog.Exceptions;

namespace InventoryManagementSystem.Extensions;

/// <summary>
/// Extensino methods to log messages
/// </summary>
public static class LoggingExtensions
{
    static LoggingExtensions()
    {
        Log.Logger = new LoggerConfiguration()
            // add console as logging target
            .WriteTo.File("log.txt")
            .Enrich.WithExceptionDetails()
            // set default minimum level
            .MinimumLevel.Debug()
            .CreateLogger();
    }

    /// <summary>
    /// Logs an error message
    /// </summary>
    /// <param name="obj"> Source object of log requesst </param>
    /// <param name="message"> Message to log </param>
    public static void Error(this object obj, string message, [CallerMemberName] string caller = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0)
    {
        Log.Error($"File: [{filePath}] Line: [{lineNumber}] Caller: [{caller}] Message: {message}");
    }

    public static void Blub(this object obj)
    {

    }

    /// <summary>
    /// Logs an error message
    /// </summary>
    /// <param name="obj"> Source object of log requesst </param>
    /// <param name="message"> Message to log </param>
    /// <param name="exception"> Exception to log </param>
    public static void Error(this object obj, string message, Exception exception, [CallerMemberName] string caller = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0)
    {
        Log.Error(exception, $"File: [{filePath}] Line: [{lineNumber}] Caller: [{caller}] Message: {message}");
    }

    /// <summary>
    /// Logs an warning message
    /// </summary>
    /// <param name="obj"> Source object of log requesst </param>
    /// <param name="message"> Message to log </param>
    public static void Warn(this object obj, string message, [CallerMemberName] string caller = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0)
    {
        Log.Warning($"File: [{filePath}] Line: [{lineNumber}] Caller: [{caller}] Message: {message}");
    }

    /// <summary>
    /// Logs an info message
    /// </summary>
    /// <param name="obj"> Source object of log requesst </param>
    /// <param name="message"> Message to log </param>
    public static void Info(this object obj, string message, [CallerMemberName] string caller = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0)
    {
        Log.Information($"File: [{filePath}] Line: [{lineNumber}] Caller: [{caller}] Message: {message}");
    }
}