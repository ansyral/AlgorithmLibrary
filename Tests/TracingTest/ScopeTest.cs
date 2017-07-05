namespace XuanLibrary.TracingTest
{
    using System.Linq;
    using XuanLibrary.Fx;

    using Xunit;

    [Collection("Tracing")]
    public class ScopeTest : TestBase
    {
        [Fact]
        public void TestBasic()
        {
            TestWrapper(() =>
            {
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
                Assert.Equal(5, Listener.Items.Count);
            });
        }
    }
}
