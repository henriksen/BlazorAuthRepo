using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Web;

namespace BlazorTest
{
    /// <summary>
    /// Delegating handler that will attach to the HttpClient and for each request it will get an access token and add it to the request headers. 
    /// </summary>
    internal class AuthHandler : DelegatingHandler
    {
        private readonly ITokenAcquisition _tokenAcquisition;

        public AuthHandler(ITokenAcquisition tokenAcquisition)
        {
            _tokenAcquisition = tokenAcquisition;
        }

        public string Scope { get; set; }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var accessToken = await _tokenAcquisition.GetAccessTokenForUserAsync(new[] {Scope}).ConfigureAwait(false);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            return await base.SendAsync(request, cancellationToken);
        }
    }
}

