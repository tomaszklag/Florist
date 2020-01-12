using System.Threading.Tasks;

namespace Core.Application.Command
{
    public interface ICommandHandler<T> where T : ICommand
    {
        Task HandleAsync(T command);
    }
}
