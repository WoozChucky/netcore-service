using System;
using System.Threading.Tasks;
using Common;
using Common.Events;

namespace MyTransportLayer
{
    public interface ITransportLayer : IDisposable
    {
        event EventHandler<RequestMessageEventArgs> OnRequestReceived;

        void Start();
        void Stop();

        Task SendResponse(Guid correlationId, ResponseMessage response);
    }
}
