using System.Threading.Tasks;

namespace Core.Application.Command
{
    public interface ICommandBus
    {
        Task SendAsync<T>(T command) where T : ICommand;
    }
}
