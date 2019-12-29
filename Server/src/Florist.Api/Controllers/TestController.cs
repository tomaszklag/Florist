using Florist.Services.Dispatchers;
using Florist.Services.Products;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Florist.Api.Controllers
{
    public class TestController : ApiController
    {
        public TestController(IDispatcher dispatcher) : base(dispatcher)
        {
        }

        [Route("")]
        public IActionResult Get() => Ok("Api works!");

        [HttpPost]
        public async Task<IActionResult> Test()
        {
            await _dispatcher.SendAsync(new TestCreateCommand());
            return NoContent();
        }
    }
}
