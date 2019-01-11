using System;
using System.Threading.Tasks;
using Common;
using Common.Events;

namespace MyEngine
{
    public class Engine : IEngine
    {
        public event EventHandler<ResponseMessageEventArgs> OnRequestProcessed;

        public void Initialize()
        {
            
        }

        public void Run()
        {
            
        }

        public void Stop()
        {
            
        }

        public async Task ProcessRequest(Guid correlationId, RequestMessage request)
        {
            // TODO: Actually process the request...


            // Then send response back to transport layer
            OnRequestProcessed?.Invoke(this, new ResponseMessageEventArgs
            {
                CorrelationId = Guid.NewGuid(),
                Response = new ResponseMessage
                {
                    Success = true,
                    Content = new byte[] {0x0A, 0x0B}
                }
            });

        }
    }
}
