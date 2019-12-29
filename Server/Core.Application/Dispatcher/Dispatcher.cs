using Core.Application.Command;
using System.Threading.Tasks;

namespace Core.Application.Dispatcher
{
    public class Dispatcher : IDispatcher
    {
        private readonly ICommandBus _commandBus;

        public Dispatcher(ICommandBus commandBus)
        {
            _commandBus = commandBus;
        }

        public async Task SendAsync<TCommand>(TCommand command) where TCommand : ICommand
            => await _commandBus.SendAsync(command);

        //public async Task<TResult> QueryAsync<TResult>(IQuery<TResult> query)
        //    => await _queryBus.QueryAsync(query);

        
    }
}
