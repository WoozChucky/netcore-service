namespace Common
{
    public sealed class ResponseMessage
    {
        public bool Success { get; set; }
        public byte[] Content { get; set; }
    }
}
