using Core.Domain.Events;

namespace Core.Application.Command
{
    public class CommandEvent<T> where T : IEvent
    {
        public CommandEvent(T @event)
        {
            Event = @event;
        }

        public T Event { get; }
    }
}
