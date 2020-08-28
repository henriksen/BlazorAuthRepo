using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Identity.Web;

namespace BlazorTest
{
    public interface ITestClientWithAuthIncluded
    {
        Task<string> GetProfileAsync();
    }

    public class TestClientWithAuthIncluded : ITestClientWithAuthIncluded
    {
        private readonly HttpClient _httpClient;
        private readonly ITokenAcquisition _tokenAcquisition;

        public TestClientWithAuthIncluded(HttpClient httpClient, ITokenAcquisition tokenAcquisition)
        {
            _httpClient = httpClient;
            _tokenAcquisition = tokenAcquisition;
        }

        public async Task<string> GetProfileAsync()
        {
            var uri = "https://graph.microsoft.com/v1.0/me";

            var accessToken = await _tokenAcquisition.GetAccessTokenForUserAsync(new[] { "User.Read" }).ConfigureAwait(false);

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var responseString = await _httpClient.GetStringAsync(uri);

            return responseString;
        }
    }





    public interface ITestClientWithoutAuth
    {
        Task<string> GetProfileAsync();
    }

    public class TestClientWithoutAuth : ITestClientWithoutAuth
    {
        private readonly HttpClient _httpClient;

        public TestClientWithoutAuth(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetProfileAsync()
        {
            var uri = "https://graph.microsoft.com/v1.0/me";

            var responseString = await _httpClient.GetStringAsync(uri);

            return responseString;
        }
    }
}
