using Florist.Infrastructure.Cqrs.Dispatchers;
using Microsoft.AspNetCore.Mvc;

namespace Florist.Api.Controllers
{
    public class HomeController : ApiController
    {
        public HomeController(IDispatcher dispatcher) : base(dispatcher)
        {
        }

        [Route("")]
        public IActionResult Get() => Ok("Api works!");
    }
}
