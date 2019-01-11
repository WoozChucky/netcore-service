using System;

namespace Common.Events
{
    public sealed class ResponseMessageEventArgs : EventArgs
    {
        public Guid CorrelationId { get; set; }
        public ResponseMessage Response { get; set; }
    }
}
