using Core.Application.Command;
using Core.Application.Event;
using Core.Domain.Events;
using System.Threading.Tasks;

namespace Core.Application.Dispatcher
{
    public class Dispatcher : IDispatcher
    {
        private readonly ICommandBus _commandBus;
        private readonly IEventBus _eventBus;

        public Dispatcher(ICommandBus commandBus,
                          IEventBus eventBus)
        {
            _commandBus = commandBus;
            _eventBus = eventBus;
        }

        public async Task SendAsync<TCommand>(TCommand command) where TCommand : ICommand
            => await _commandBus.SendAsync(command);

        //public async Task<TResult> QueryAsync<TResult>(IQuery<TResult> query)
        //    => await _queryBus.QueryAsync(query);

        public async Task PublishAsync<TEvent>(TEvent @event) where TEvent : CommandEvent<IEvent>
            => await _eventBus.PublishAsync(@event);
    }
}
