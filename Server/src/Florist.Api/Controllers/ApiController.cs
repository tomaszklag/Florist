using Florist.Infrastructure.Cqrs.Dispatchers;
using Microsoft.AspNetCore.Mvc;

namespace Florist.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public abstract class ApiController : ControllerBase
    {
        protected readonly IDispatcher _dispatcher;

        public ApiController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }
    }
}
