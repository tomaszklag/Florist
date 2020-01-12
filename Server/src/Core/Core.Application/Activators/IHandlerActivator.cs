using Core.Application.Command;

namespace Core.Application.Activators
{
    public interface IHandlerActivator
    {
        dynamic CreateCommandHandler<T>(T command) where T : ICommand;
    }
}
