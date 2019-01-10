using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using MyEngine;
using MyTransportLayer;

namespace HostedApp
{
    public class MyService : IHostedService, IDisposable
    {
        private readonly IEngine _engine;
        private readonly ITransportLayer _transportLayer;


        public MyService(
            IEngine engine,
            ITransportLayer transportLayer)
        {
            _engine = engine;
            _transportLayer = transportLayer;
        }
        public Task StartAsync(CancellationToken cancellationToken = default)
        {
            Console.WriteLine("Service Started...");

            _engine?.Initialize();
            _engine?.Run();

            _transportLayer?.Start();

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken = default)
        {
            Console.WriteLine("Service Stopped...");

            _transportLayer?.Stop();
            _engine?.Stop();

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _transportLayer?.Dispose();
        }
    }
}
