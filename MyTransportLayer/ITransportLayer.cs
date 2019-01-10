using System;

namespace MyTransportLayer
{
    public interface ITransportLayer : IDisposable
    {
        void Start();
        void Stop();
    }
}
