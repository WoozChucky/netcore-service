using System;

namespace Common.Events
{
    public sealed class RequestMessageEventArgs : EventArgs
    {
        public Guid CorrelationId { get; set; }
        public RequestMessage Request { get; set; }
    }
}
