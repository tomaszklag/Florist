using Core.Application.Command;
using Core.Application.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Core.Application.Activator
{
    public static class HandlerContainers
    {
        public static readonly IDictionary<Type, TypeInfo> CommandHandlerResolver = typeof(ICommandHandler<>).GetRegistredHandlers()
            .ToDictionary(x => x.GetGenericInterfaceTypeArgument(typeof(ICommandHandler<>)), x => x);

        public static readonly IDictionary<Type, IEnumerable<TypeInfo>> EventHandlersResolver = typeof(IEventHandler<>).GetRegistredHandlers()
            .GroupBy(x => x.GetGenericInterfaceTypeArgument(typeof(IEventHandler<>)))
            .ToDictionary(g => g.Key, g => g.AsEnumerable());

        private static Type GetGenericInterfaceTypeArgument(this TypeInfo typeInfo, Type interfaceType)
            => typeInfo.ImplementedInterfaces.Where(it => it.GetGenericTypeDefinition() == interfaceType)
                                             .First().GenericTypeArguments
                                             .First();

        private static IEnumerable<TypeInfo> GetRegistredHandlers(this Type handlerType)
            => AppDomain.CurrentDomain.GetAssemblies()
                                      .Where(a => a.FullName.Contains("Application"))
                                      .SelectMany(t => t.DefinedTypes)
                                      .Where(x => x.GetInterfaces().Any(it => it.IsGenericType && it.GetGenericTypeDefinition() == handlerType));
    }
}
