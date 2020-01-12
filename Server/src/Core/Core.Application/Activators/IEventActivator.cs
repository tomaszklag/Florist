using Core.Application.Command;
using Core.Domain.Events;

namespace Core.Application.Activators
{
    public interface IEventActivator
    {
        CommandEvent<IEvent> CreateCommandEvent(IEvent @event);
    }
}
