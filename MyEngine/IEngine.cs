using System;
using System.Threading.Tasks;
using Common;
using Common.Events;

namespace MyEngine
{
    public interface IEngine
    {
        event EventHandler<ResponseMessageEventArgs> OnRequestProcessed;

        void Initialize();
        void Run();
        void Stop();

        Task ProcessRequest(Guid correlationId, RequestMessage request);
    }
}
