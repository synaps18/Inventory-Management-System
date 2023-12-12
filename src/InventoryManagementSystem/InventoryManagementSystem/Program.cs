using System.Net.Mime;
using System.Runtime.CompilerServices;
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
            Console.Title = "Inventory Management System";
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.White;

            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            ConfigureContainer();

            Container.Resolve<IUserInterface>();
            
            await KeepAlive();
        }

        private static readonly Container Container = new();

        private static void ConfigureContainer()
        {
            Container.Register<IUserInterface, ConsoleInterface>();
            Container.Register<IInventoryManagementService, InventoryManagementService>();
            Container.Register<IPersistService, PersistService>();

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

        private static CancellationTokenSource _keepAliveCts = new();

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