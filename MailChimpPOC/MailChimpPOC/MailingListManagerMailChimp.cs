using MailChimpPOC.HttpClient;
using MailChimpPOC.JSonConverter;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MailChimpPOC
{
    public class MailingListManagerMailChimp
    {
        private readonly IHttpClient _httpClient;
        private readonly IJsonConverter _jsonConverter;

        public MailingListManagerMailChimp(IHttpClient httpClient, IJsonConverter jsonConverter)
        {
            _httpClient = httpClient;
            _jsonConverter = jsonConverter;

            _httpClient.SetAuthorizationHeaders("apikey", "bd646505f55b9b767b1acde3aca70ccf-us16");
        }

        public async Task<string> GetAllList()
        {
            string result = null;
            using (HttpResponseMessage response = await _httpClient.GetAsync("https://us16.api.mailchimp.com/3.0/lists"))
            {
                if (response.IsSuccessStatusCode)
                {
                    using (HttpContent content = response.Content)
                    {
                        result = await content.ReadAsStringAsync();
                    }
                }
            }
            return result;
        }

        public async Task<HttpResponseMessage> AddUserToList(string email, string name, string lastname, string listId)
        {
            var sampleListMember = _jsonConverter.SerializeObject(
                new
                {
                    email_address = email,
                    merge_fields =
                    new
                    {
                        FNAME = name,
                        LNAME = lastname
                    },
                    status = "subscribed"
                });

            var hashedEmail = string.IsNullOrEmpty(email) ? "" : CalculateMD5Hash(email.ToLower());
            var uri = string.Format("https://us16.api.mailchimp.com/3.0/lists/{0}/members/{1}", listId, hashedEmail);

            return await _httpClient.PostAsync(uri, new JsonEncodedContent(sampleListMember));
        }

        private static string CalculateMD5Hash(string input)
        {
            // Step 1, calculate MD5 hash from input.
            var md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            // Step 2, convert byte array to hex string.
            var sb = new StringBuilder();
            foreach (var @byte in hash)
            {
                sb.Append(@byte.ToString("X2"));
            }
            return sb.ToString();
        }
    }
}
