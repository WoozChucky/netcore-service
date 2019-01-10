using System;
using System.Threading.Tasks;

namespace HostedApp
{
    internal class Program
    {
        private static Application Application;

        private static async Task Main(string[] args)
        {
            Application = new Application();

            AppDomain.CurrentDomain.ProcessExit += CurrentDomainOnProcessExit;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;

            Console.CancelKeyPress += ConsoleOnCancelKeyPress;

            Console.WriteLine("Hello World!");

            await Application.Start(args);
        }

        private static void ConsoleOnCancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            Console.WriteLine("CancelKeyPress occurred.");
            Application.GracefullyShutdown();
        }

        private static void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Console.WriteLine("UnhandledException occurred.");
            Application.GracefullyShutdown();
        }

        private static void CurrentDomainOnProcessExit(object sender, EventArgs e)
        {
            Console.WriteLine("ProcessExit occurred.");
            Application.GracefullyShutdown();
        }
    }
}
