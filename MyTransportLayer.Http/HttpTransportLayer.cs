using System;
using System.Threading.Tasks;
using Common;
using Common.Events;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace MyTransportLayer.Http
{
    public class HttpTransportLayer : BaseTransportLayer
    {
        public override event EventHandler<RequestMessageEventArgs> OnRequestReceived;

        private readonly IWebHost _httpServer;

        public HttpTransportLayer()
        {
            _httpServer = WebHost.CreateDefaultBuilder()
                .UseKestrel()
                .UseIISIntegration()
                .UseUrls("http://0.0.0.0:8090")
                .UseStartup<HttpStartup>()
                .Build();

            HttpStartup.Request += Http_OnRequest;
        }

        private void Http_OnRequest(object sender, RequestMessageEventArgs e)
        {
            if (!IsRunning) return;
            // Here we need a func to write to the context.Response
            // HttpStartup.Request needs to pass it probably.
            OnRequestReceived(this, e);
        }

        public override async void Start()
        {
            base.Start();
            await _httpServer.StartAsync();
        }

        public override async void Stop()
        {
            base.Stop();
            await _httpServer.StopAsync();
        }

        protected override async Task SendResponse(ResponseMessage message)
        {
            
        }

        public override void Dispose()
        {
            _httpServer?.Dispose();
        }
    }
}
