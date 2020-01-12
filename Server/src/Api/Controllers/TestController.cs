using Core.Application.Dispatcher;
using Florist.Infrastructure.EventStore;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Test.Application.Commands.CreateTest;

namespace Florist.Api.Controllers
{
    public class TestController : ApiController
    {
        public TestController(IDispatcher dispatcher, IUnitOfWork unitOfWork) : base(dispatcher, unitOfWork)
        {
        }

        [Route("")]
        public IActionResult Get() => Ok("Api works!");

        [HttpPost]
        public async Task<IActionResult> Test() => await RequestAsync(new CreateTestCommand("test 1"));
    }
}
