using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using HostedApp.Extensions;
using HostedApp.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyEngine;
using MyTransportLayer;
using MyTransportLayer.Http;

namespace HostedApp
{
    internal sealed class Application
    {
        private IHost _host;
        private IHostBuilder _builder;

        private bool _isRunning;
        private readonly object _shutdownLock = new object();

        public Application()
        {
        }

        public async Task Start(string[] args)
        {
            _builder = Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddScoped<IEngine, Engine>();

                    services.AddScoped<ITransportLayer, HttpTransportLayer>();

                    services.AddHostedService<MyService>();
                })
                .UseContentRoot(Directory.GetCurrentDirectory());
            
            var isService = !(Debugger.IsAttached || ((IList)args).Contains("--console"));

            if (isService)
            {
                Console.WriteLine("Running as service...");
                //_builder.UseSystemd();
                //_builder.UseWindowsService();
                _host = _builder.BuildAsService();
            }
            else
            {
                Console.WriteLine("Running as console...");
                _host = _builder.BuildAsConsole();
            }
            
            _isRunning = true;
            await _host.RunAsync();
        }

        public void GracefullyShutdown()
        {
            lock (_shutdownLock)
            {
                if (!_isRunning) return;

                _isRunning = false;
                
                if (_host.Services == null) throw new Exception("No services registered.");

                if (!(_host.Services.GetService<IHostedService>() is MyService service))
                    throw new InvalidCastException("IHostedService couldn't be casted to MyService");

                // Stop the service
                service.StopAsync().Wait();

                // Stop the host
                _host.StopAsync().Wait();
            }
        }
    }
}
