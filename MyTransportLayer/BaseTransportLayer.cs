using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Common;
using Common.Events;

namespace MyTransportLayer
{
    public abstract class BaseTransportLayer : ITransportLayer
    {
        public abstract event EventHandler<RequestMessageEventArgs> OnRequestReceived;

        protected readonly ConcurrentDictionary<Guid, RequestMessage> PendingRequests;

        protected bool IsRunning;

        protected BaseTransportLayer()
        {
            PendingRequests = new ConcurrentDictionary<Guid, RequestMessage>();
        }

        public virtual void Start()
        {
            IsRunning = true;
        }

        public virtual void Stop()
        {
            IsRunning = false;
        }

        public async Task SendResponse(Guid correlationId, ResponseMessage response)
        {
            PendingRequests.TryGetValue(correlationId, out var requestContent);

            if (requestContent == null)
            {
                Console.WriteLine($"No pending request was found with Id (${correlationId}). Response not sent.");
                return;
            }

            await SendResponse(response);
            
            PendingRequests.TryRemove(correlationId, out _);
        }

        protected abstract Task SendResponse(ResponseMessage message);

        public abstract void Dispose();
    }
}
