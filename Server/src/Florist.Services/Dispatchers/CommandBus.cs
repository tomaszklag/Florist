using Florist.Core.Types;
using System;
using System.Threading.Tasks;

namespace Florist.Services.Dispatchers
{
    public class CommandBus : ICommandBus
    {
        private readonly IServiceProvider _provider;

        public CommandBus(IServiceProvider provider)
        {
            _provider = provider;
        }

        public async Task SendAsync<T>(T command) where T : ICommand
        {
            ICommandHandler<T> handler = _provider.Resolve(typeof(ICommandHandler<T>));

            if (handler is null)
            {
                throw new ArgumentException($"Command handler: '{typeof(T).Name} was not found.'",
                    nameof(handler));
            }

            await handler.HandleAsync(command);
        }
    }
}
