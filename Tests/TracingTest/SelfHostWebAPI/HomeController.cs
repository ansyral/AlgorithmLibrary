namespace XuanLibrary.TracingTest
{
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web.Http;

    using XuanLibrary.Fx;

    [RoutePrefix("api/home")]
    public class HomeController : ApiController
    {
        [Route("{id}")]
        [HttpGet]
        public async Task<HttpResponseMessage> Index(int id)
        {
            using (new TracingScope("API: get index"))
            {
                return await Task.Run(() =>
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "test");
                });
            }
        }
    }
}
