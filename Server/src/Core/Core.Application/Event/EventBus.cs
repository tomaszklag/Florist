using Core.Application.Activators;
using Core.Domain.Events;
using System.Threading.Tasks;

namespace Core.Application.Event
{
    public class EventBus : IEventBus
    {
        private readonly IHandlerActivator _handlerActivator;

        public EventBus(IHandlerActivator handlerActivator)
        {
            _handlerActivator = handlerActivator;
        }

        public async Task PublishAsync<TEvent>(TEvent @event) where TEvent : IEvent
        {
            var handlers = _handlerActivator.ResolveEventHandlers(@event);

            foreach (var handler in handlers)
            {
                await handler.HandleAsync(@event);
            }
        }
    }
}
