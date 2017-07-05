namespace XuanLibrary.TracingTest
{
    using XuanLibrary.Fx;

    using Xunit;

    public class ScopeTest
    {
        [Fact]
        public void TestBasic()
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
            TraceExt.Close();
        }
    }
}
