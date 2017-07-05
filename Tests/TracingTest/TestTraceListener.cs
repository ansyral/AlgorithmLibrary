namespace XuanLibrary.TracingTest
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    using XuanLibrary.Fx;

    public class TestTraceListener : TraceListener
    {
        public override string Name => nameof(TestTraceListener);

        public List<string> Items { get; } = new List<string>();

        public override void Write(string message)
        {
            Items.Add(message);
        }

        public override void WriteLine(string message)
        {
            Items.Add(message);
        }

        public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string message)
        {
            Items.Add(message);
        }
    }
}
