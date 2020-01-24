using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Application.Activator
{
    public static class DynamicResolver
    {
        public static dynamic ResolveFirstOrDefault(this IServiceProvider provider, Type requestedType)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(a => a.FullName.Contains("Application"));
            var assembliesTypes = assemblies.SelectMany(t => t.DefinedTypes);

            foreach (var type in assembliesTypes)
            {
                if (type.ImplementedInterfaces.Any(i => i == requestedType))
                    return ActivatorUtilities.CreateInstance(provider, type.AsType());
            }

            return null;
        }

        public static List<dynamic> ResolveAll(this IServiceProvider provider, Type requestedType)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(a => a.FullName.Contains("Application"));
            var assembliesTypes = assemblies.SelectMany(t => t.DefinedTypes);
            var resolvedTypes = new List<dynamic>();

            foreach (var type in assembliesTypes)
            {
                if (type.ImplementedInterfaces.Any(i => i == requestedType))
                    resolvedTypes.Add(ActivatorUtilities.CreateInstance(provider, type.AsType()));
            }

            return null;
        }
    }
}
