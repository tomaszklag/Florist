using Core.Application.Activator;
using Core.Application.Command;
using Core.Application.Event;
using Core.Domain.Events;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
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

        public ICommandHandler<T> ResolveCommandHandler<T>(T command) where T : ICommand
            => HandlerContainers.CommandHandlerResolver.TryGetValue(typeof(T), out var handler) 
                ? ActivatorUtilities.CreateInstance(_provider.CreateScope().ServiceProvider, handler.AsType()) as ICommandHandler<T> 
                : null;

        public IEnumerable<IEventHandler<T>> ResolveEventHandlers<T>(T @event) where T : IEvent
            => ResolveAll(typeof(IEventHandler<T>)) as IEnumerable<IEventHandler<T>>;

        private IEnumerable<dynamic> ResolveAll(Type requestedType)
        {
            var assembly = Assembly.GetExecutingAssembly();

            var resolvedInstances = new List<dynamic>();

            foreach (var type in assembly.DefinedTypes)
            {
                if (type.ImplementedInterfaces.Contains(requestedType))
                {
                    var instance = ActivatorUtilities.CreateInstance(_provider, type.AsType());
                    resolvedInstances.Add(instance);
                }
            }

            return resolvedInstances;
        }
    }
}
