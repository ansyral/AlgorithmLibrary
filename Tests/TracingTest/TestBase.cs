namespace XuanLibrary.TracingTest
{
    using System;
    using System.Linq;

    using XuanLibrary.Fx;

    public class TestBase
    {
        protected TestTraceListener Listener
        {
            get
            {
                return TraceExt.Listeners.OfType<TestTraceListener>().Single();
            }
        }
        protected void TestWrapper(Action testAction)
        {
            try
            {
                Init();
                testAction();
            }
            finally
            {
                Clear();
            }
        }

        private void Init()
        {
            var listener = TraceExt.Listeners.Add(new TestTraceListener());
        }

        private void Clear()
        {
            TraceExt.Listeners.Remove(nameof(TestTraceListener));
        }
    }
}
