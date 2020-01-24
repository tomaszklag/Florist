using Core.Application.Command;
using Core.Domain.Events;
using System.Threading.Tasks;

namespace Core.Application.Dispatcher
{
    public interface IDispatcher
    {
        Task SendAsync<TCommand>(TCommand command) where TCommand : ICommand;
        //Task<TResult> QueryAsync<TResult>(IQuery<TResult> query);
        Task PublishAsync<TEvent>(TEvent @event) where TEvent : IEvent;
    }
}
