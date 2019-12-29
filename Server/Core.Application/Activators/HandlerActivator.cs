using Core.Application.Command;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace Core.Application.Activators
{
    public class HandlerActivator : IHandlerActivator
    {
        private readonly IServiceProvider _provider;

        public HandlerActivator(IServiceProvider provider)
        {
            _provider = provider;
        }

        public dynamic CreateCommandHandler<T>(T command) where T : ICommand
            => Resolve(typeof(ICommandHandler<T>));

        private dynamic Resolve(Type requestedType)
        {
            var assembly = Assembly.GetExecutingAssembly();

            foreach (var type in assembly.DefinedTypes)
            {
                if (type.ImplementedInterfaces.Contains(requestedType))
                    return ActivatorUtilities.CreateInstance(_provider, type.AsType());
            }

            return null;
        }
    }
}
