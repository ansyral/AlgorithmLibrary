namespace XuanLibrary.TracingTest
{
    using Microsoft.Owin.Hosting;
    using XuanLibrary.Fx;
    using XuanLibrary.Utility;

    using Xunit;

    [Collection("Tracing")]
    public class AmbientContextTest : TestBase
    {
        [Fact]
        public void TestBasic()
        {
            TestWrapper(() =>
            {
                string baseAddress = "http://localhost:9000/";

                // Start OWIN host 
                using (WebApp.Start<Startup>(url: baseAddress))
                {
                    var ambientContext = AmbientContextCrossServices.GetOrCreateCurrent();
                    string correlationId = ambientContext.CorrelationID;
                    var response = AmbientContextFlowToDownStreamHttpClient.PostAsync<string>(baseAddress + "api/home/1").Result;

                    Assert.Equal("test", response);
                    Assert.Equal(1, Listener.Items.Count);
                    var tracingItem = JsonUtility.FromJsonString<TracingItem>(Listener.Items[0]);
                    Assert.True(tracingItem.CorrelationId.StartsWith(correlationId));
                    Assert.Equal("API: get index", tracingItem.Scope);
                }
            });
        }
    }
}
