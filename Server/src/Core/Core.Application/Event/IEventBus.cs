using Core.Application.Command;
using Core.Domain.Events;
using System.Threading.Tasks;

namespace Core.Application.Event
{
    public interface IEventBus
    {
        Task PublishAsync<TEvent>(TEvent @event) where TEvent : IEvent;
    }
}
