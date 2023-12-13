using DryIoc;
using InventoryManagementSystem.Extensions;
using InventoryManagementSystem.Interfaces;
using InventoryManagementSystem.Services;
using InventoryManagementSystem.Ui;

namespace InventoryManagementSystem
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            ConfigureContainer();

            Container.Resolve<IUserInterface>();
            
            await KeepAlive();
        }

        private static readonly Container Container = new();

        private static void ConfigureContainer()
        {
            Container.Register<IUserInterface, ConsoleInterface>(Reuse.Singleton);
            Container.Register<IInventoryManagementService, InventoryManagementService>(Reuse.Singleton);
            Container.Register<IPersistService, PersistService>(Reuse.Singleton);
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is Exception exceptionObject)
            {
                sender.Error("Unhandled exception", exceptionObject);
                return;
            }

            sender.Error("Unhandled exception");
        }

        private static readonly CancellationTokenSource _keepAliveCts = new();

        private static async Task KeepAlive()
        {
            await Task.Run(async () =>
            {
                while (!_keepAliveCts.IsCancellationRequested)
                {
                    await Task.Delay(100);
                }
                // ReSharper disable once FunctionNeverReturns
            }, _keepAliveCts.Token);
        }
    }
}