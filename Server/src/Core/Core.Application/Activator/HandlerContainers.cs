using Core.Application.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Core.Application.Activator
{
    public static class HandlerContainers
    {
        public static readonly IDictionary<Type, TypeInfo> CommandHandlerResolver =
            AppDomain.CurrentDomain.GetAssemblies().Where(a => a.FullName.Contains("Application"))
                                   .SelectMany(t => t.DefinedTypes)
                                   .Where(x => x.GetInterfaces().Any(it => it.IsGenericType && it.GetGenericTypeDefinition() == typeof(ICommandHandler<>)))
                                   .ToDictionary(x => x.ImplementedInterfaces.Where(it => it.GetGenericTypeDefinition() == typeof(ICommandHandler<>))
                                                                             .First()
                                                                             .GenericTypeArguments
                                                                             .First(), x => x);
    }
}
