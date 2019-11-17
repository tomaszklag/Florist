using Florist.Core.Types;
using System;
using System.Threading.Tasks;

namespace Florist.Infrastructure.Cqrs.Dispatchers
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IServiceProvider _provider;

        public CommandDispatcher(IServiceProvider provider)
        {
            _provider = provider;
        }

        public async Task SendAsync<T>(T command) where T : ICommand
        {
            dynamic handler = _provider.Resolve(typeof(ICommandHandler<T>));

            if (handler is null)
            {
                throw new ArgumentException($"Command handler: '{typeof(T).Name} was not found.'",
                    nameof(handler));
            }

            await handler.HandleAsync(command);
        }

        public async Task<TResult> SendAndResponseDataAsync<TResult>(ICommand<TResult> command)
        {
            var handlerType = typeof(ICommandHandler<,>)
                .MakeGenericType(command.GetType(), typeof(TResult));

            dynamic handler = _provider.Resolve(handlerType);

            if (handler is null)
            {
                throw new ArgumentException($"Command handler: '{handlerType.Name} was not found.'",
                    nameof(handler));
            }

            return await handler.HandleAsync((dynamic)command);
        }
    }
}
