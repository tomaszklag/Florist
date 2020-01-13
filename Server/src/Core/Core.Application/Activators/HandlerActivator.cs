using Core.Application.Command;
using Core.Application.Event;
using Core.Domain.Events;
using Microsoft.Extensions.DependencyInjection;
using System;
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
            => ResolveFirstOrDefault(typeof(ICommandHandler<T>)) as ICommandHandler<T>;

        public IEnumerable<IEventHandler<T>> ResolveEventHandlers<T>(T @event) where T : IEvent
            => ResolveAll(typeof(IEventHandler<T>)) as IEnumerable<IEventHandler<T>>;

        private dynamic ResolveFirstOrDefault(Type requestedType)
        {
            var assembly = Assembly.GetCallingAssembly(); //tutaj trzeba przeszukać inne assembly. Cze nie lepiej byłoby zrobić handlery jako abstrakcyjne?

            foreach (var type in assembly.DefinedTypes)
            {
                if (type.ImplementedInterfaces.Contains(requestedType))
                    return ActivatorUtilities.CreateInstance(_provider, type.AsType());
            }

            return null;
        }

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
