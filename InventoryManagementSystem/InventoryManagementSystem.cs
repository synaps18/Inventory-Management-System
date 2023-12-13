using DryIoc;
using InventoryManagementSystem.Extensions;
using InventoryManagementSystem.Interfaces;
using InventoryManagementSystem.Services;
using InventoryManagementSystem.Ui;

namespace InventoryManagementSystem;

/// <summary>
///     System to manaage an inventory of products
/// </summary>
internal class InventoryManagementSystem
{
    private static readonly CancellationTokenSource KeepAliveCts = new();
    private static readonly Container Container = new();

    /// <summary>
    ///     Configures the IOC Container
    /// </summary>
    private static void ConfigureContainer()
    {
        Container.Register<IUserInterface, ConsoleInterface>(Reuse.Singleton);
        Container.Register<IInventoryManagementService, InventoryManagementService>(Reuse.Singleton);
        Container.Register<IPersistService, PersistService>(Reuse.Singleton);
    }

    /// <summary>
    ///     Handles unhandled exception on whole app domain.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
        if (e.ExceptionObject is Exception exceptionObject)
        {
            sender.Error("Unhandled exception", exceptionObject);
            return;
        }

        sender.Error("Unhandled exception");
    }

    /// <summary>
    ///     Keeps application alive
    /// </summary>
    /// <returns></returns>
    private static async Task KeepAlive()
    {
        await Task.Run(async () =>
        {
            while (!KeepAliveCts.IsCancellationRequested) await Task.Delay(100);
            // ReSharper disable once FunctionNeverReturns
        }, KeepAliveCts.Token);
    }

    /// <summary>
    /// Entry Point
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    private static async Task Main(string[] args)
    {
        AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

        ConfigureContainer();

        Container.Resolve<IUserInterface>();

        await KeepAlive();
    }
}