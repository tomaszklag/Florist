using Florist.Core.Types;
using System.Threading.Tasks;

namespace Florist.Infrastructure.Cqrs.Dispatchers
{
    public interface IDispatcher
    {
        Task SendAsync<TCommand>(TCommand command) where TCommand : ICommand;
        Task<TResult> SendAndResponseDataAsync<TResult>(ICommand<TResult> command);
        Task<TResult> QueryAsync<TResult>(IQuery<TResult> query);
    }
}
