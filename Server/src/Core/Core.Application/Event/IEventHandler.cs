using Core.Domain.Events;
using System.Threading.Tasks;

namespace Core.Application.Event
{
    public interface IEventHandler<T> where T : IEvent
    {
        Task HandleAsync(T @event);
    }
}
