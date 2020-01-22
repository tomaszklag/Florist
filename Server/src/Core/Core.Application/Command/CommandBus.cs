using Core.Application.Activators;
using System;
using System.Threading.Tasks;

namespace Core.Application.Command
{
    public class CommandBus : ICommandBus
    {
        private readonly IHandlerActivator _handlerActivator;

        public CommandBus(IHandlerActivator handlerActivator)
        {
            _handlerActivator = handlerActivator;
        }

        public async Task SendAsync<T>(T command) where T : ICommand
        {
            dynamic handler = _handlerActivator.ResolveCommandHandler(command);
            if (handler is null)
            {
                throw new ArgumentException($"Command handler: '{command.GetType().Name} was not found.'",
                    nameof(handler));
            }

            var result = (Task)handler.GetType().GetMethod("HandleAsync").Invoke(handler, new object[] { command });
            await result;

            //await handler.HandleAsync(command);
        }
    }
}
