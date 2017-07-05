namespace XuanLibrary.TracingTest
{
    using System.Collections.Generic;
    using System.Linq;
    using XuanLibrary.Fx;

    using Xunit;

    [Collection("Tracing")]
    public class ScopeTest
    {
        [Fact]
        public void TestBasic()
        {
            try
            {
                Init();
                using (var scouter = new TracingScope("A"))
                {
                    Assert.Equal("A", TracingScope.GetScopeName());
                    TraceExt.TraceInfo("This is in the scope A.", null);
                    using (var scinner = new TracingScope("B"))
                    {
                        Assert.Equal("A.B", TracingScope.GetScopeName());
                        TraceExt.TraceInfo("This is in the scope B.", null);
                    }
                    Assert.Equal("A", TracingScope.GetScopeName());
                    TraceExt.TraceInfo("This is in the scope A again.", null);
                }
                Assert.Equal(5, TraceExt.Listeners.OfType<TestTraceListener>().Single().Items.Count);
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
