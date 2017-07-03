namespace XuanLibrary.Fx
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading;
    using System.Threading.Tasks;

    using XuanLibrary.Utility;

    public class AmbientContextInheritFromUpStreamMessageHandler : DelegatingHandler
    {
        private const string HeaderName = "AMBIENT_CONTEXT";

        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            string serializedJson = HeaderHelper.GetHeader(request.Headers, HeaderName);

            using (AmbientContextCrossServices context = string.IsNullOrEmpty(serializedJson) ? null : AmbientContextCrossServices.Initialize(serializedJson))
            {
                HttpResponseMessage response = await base.SendAsync(request, cancellationToken);
                return response;
            }
        }
    }

    public static class AmbientContextFlowToDownStreamHttpClient
    {
        private const string HeaderName = "AMBIENT_CONTEXT";

        /// <summary>
        /// sample case of HTTP Post
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="requestUri"></param>
        /// <returns></returns>
        public static async Task<T> PostAsync<T>(string requestUri)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUri);
            var current = AmbientContextCrossServices.TryGetCurrent();
            if (current != null)
            {
                var branch = current.CreateBranch();
                request.Headers.Add(HeaderName, branch.ToString());
            }
            using (var response = (HttpWebResponse)(await request.GetResponseAsync()))
            using (var stream = response.GetResponseStream())
            {
                if (stream != null)
                {
                    using (var reader = new StreamReader(stream))
                    {
                        return JsonUtility.Deserialize<T>(reader);
                    }
                }
                return default(T);
            }
        }
    }

    internal static class HeaderHelper
    {
        public static string GetHeader(HttpHeaders headers, string name)
        {
            IEnumerable<string> matches = null;
            if (headers.TryGetValues(name, out matches))
            {
                return matches.First();
            }
            return null;
        }

        public static void SetHeader(HttpHeaders header, string name, string value)
        {
            header.Remove(name);
            header.Add(name, value);
        }
    }
}
