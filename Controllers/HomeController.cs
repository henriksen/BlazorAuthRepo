using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Web;

namespace BlazorTest.Controllers
{
    [Authorize]
    [AuthorizeForScopes(Scopes = new string[] { "User.Read" })]
    public class HomeController : Controller
    {
        private readonly ITestClientWithoutAuth _testClient;


        public HomeController(ITestClientWithoutAuth testClient)
        {
            _testClient = testClient;
        }

        [Route("Home/Index")]

        public async Task<IActionResult> Index()
        {
            var result = await _testClient.GetProfileAsync();
            return Ok(result);
        }

    }
}
