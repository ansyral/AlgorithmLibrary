namespace XuanLibrary.TracingTest
{
    using System.Collections.Generic;
    using System.Configuration;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using XuanLibrary.Fx;

    using Xunit;

    [Collection("Tracing")]
    public class ListenerTest
    {
        [Fact]
        public void TestRollingFile()
        {
            var tasks = Enumerable.Range(0, 20).Select(t => Task.Run(() => TraceExt.TraceError($"this is thread {t}", t, null, null)));
            Task.WhenAll(tasks).Wait();
            TraceExt.Close();

            // check trace log file
            string curDir = Directory.GetCurrentDirectory();
            var tempFolder = Path.Combine(curDir, "temp");
            var commitedFolder = Path.Combine(curDir, "commited");
            Assert.True(Directory.Exists(tempFolder));
            Assert.True(Directory.Exists(commitedFolder));
            Assert.NotEmpty(new DirectoryInfo(commitedFolder).EnumerateFiles());
        }
    }
}
