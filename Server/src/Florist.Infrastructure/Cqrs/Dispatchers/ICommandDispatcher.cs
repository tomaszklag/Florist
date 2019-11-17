using Florist.Core.Types;
using System.Threading.Tasks;

namespace Florist.Infrastructure.Cqrs.Dispatchers
{
    public interface ICommandDispatcher
    {
        Task SendAsync<T>(T command) where T : ICommand;
        Task<TResult> SendAndResponseDataAsync<TResult>(ICommand<TResult> command);
    }
}
