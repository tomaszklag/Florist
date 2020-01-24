using Core.Application.Command;
using Core.Application.Dispatcher;
using Florist.Infrastructure.EventStore;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Florist.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public abstract class ApiController : ControllerBase
    {
        protected readonly IDispatcher _dispatcher;
        private readonly IUnitOfWork _unitOfWork;

        public ApiController(IDispatcher dispatcher,
                             IUnitOfWork unitOfWork)
        {
            _dispatcher = dispatcher;
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> RequestAsync<T>(T command) where T: ICommand
        {
            try
            {
                await _dispatcher.SendAsync(command);
                await _unitOfWork.CommitAsync();
                return Accepted();
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackAsync();
                throw e;
            }
        }
    }
}
