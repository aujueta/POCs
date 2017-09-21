namespace MailChimpPOC.HttpClient
{
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;

    public class HttpClientWrapper : IHttpClient
    {
        private readonly HttpClient _httpClient;


        public HttpClientWrapper(System.Net.Http.HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> PostAsync(string url, FormUrlEncodedContent content)
        {
            return await SendAsync(url, content);
        }

        public async Task<HttpResponseMessage> PostAsync(string url, JsonEncodedContent content)
        {
            return await SendAsync(url, content);
        }

        private async Task<HttpResponseMessage> SendAsync(string url, HttpContent content)
        {
            using (_httpClient)
            {
                var result = await _httpClient.PutAsync(url, content);
                return result;
            }
        }

        public async Task<HttpResponseMessage> GetAsync(string url)
        {
            using (_httpClient)
            {
                _httpClient.DefaultRequestHeaders.Accept.Clear();
                var result = await _httpClient.GetAsync(url);
                return result;
            }
        }

        public void SetAuthorizationHeaders(string key, string value)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(key, value);
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}
