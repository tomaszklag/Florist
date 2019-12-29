using System.Threading.Tasks;

namespace Florist.Core.Types
{
    public interface ICommandHandler<T> where T : ICommand
    {
        Task HandleAsync(T command);
    }
}
