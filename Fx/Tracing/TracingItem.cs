namespace XuanLibrary.Fx
{
    using System;

    public class TracingItem
    {
        public string CorrelationId { get; set; }

        public string Message { get; set; }

        public string Scope { get; set; }

        public object Data { get; set; }

        public DateTime Timestamp { get; set; }

        public TracingLevel Level { get; set; }
    }

    public enum TracingLevel
    {
        Error,
        Warning,
        Info,
    }
}
