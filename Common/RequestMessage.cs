using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public sealed class RequestMessage
    {
        public Dictionary<string, string> Headers { get; set; }
        public byte[] Content { get; set; }
        public string Path { get; set; }
    }
}
