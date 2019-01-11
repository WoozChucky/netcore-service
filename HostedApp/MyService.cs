using System;
using System.Threading;
using System.Threading.Tasks;
using Common.Events;
using Microsoft.Extensions.Hosting;
using MyEngine;
using MyTransportLayer;

namespace HostedApp
{
    public sealed class MyService : IHostedService, IDisposable
    {
        private readonly IEngine _engine;
        private readonly ITransportLayer _transportLayer;


        public MyService(
            IEngine engine,
            ITransportLayer transportLayer)
        {
            _engine = engine ?? throw new ArgumentNullException(nameof(engine));
            _transportLayer = transportLayer ?? throw new ArgumentNullException(nameof(transportLayer));

            _engine.OnRequestProcessed += EngineOnRequestProcessed;
            _transportLayer.OnRequestReceived += TransportLayerOnRequestReceived;
        }
        public Task StartAsync(CancellationToken cancellationToken = default)
        {
            Console.WriteLine("Service Started...");

            _engine.Initialize();
            _engine.Run();
            
            _transportLayer.Start();

            return Task.CompletedTask;
        }

        private async void TransportLayerOnRequestReceived(object sender, RequestMessageEventArgs e)
        {
            await _engine.ProcessRequest(e.CorrelationId, e.Request);
        }

        private async void EngineOnRequestProcessed(object sender, ResponseMessageEventArgs e)
        {
            await _transportLayer.SendResponse(e.CorrelationId, e.Response);
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
            _transportLayer.Dispose();
        }
    }
}
