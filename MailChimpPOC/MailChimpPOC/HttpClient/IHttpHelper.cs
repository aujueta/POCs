using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace MailChimpPOC.HttpClient
{
    public interface IHttpClient : IDisposable
    {
        Task<HttpResponseMessage> PostAsync(string url, FormUrlEncodedContent content);

        Task<HttpResponseMessage> PostAsync(string url, JsonEncodedContent content);

        Task<HttpResponseMessage> GetAsync(string url);

        void SetAuthorizationHeaders(string key, string value);
    }
}
