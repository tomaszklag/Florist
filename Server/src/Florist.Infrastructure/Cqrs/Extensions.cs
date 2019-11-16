
using Florist.Infrastructure.Cqrs.Dispatchers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace Florist.Infrastructure.Cqrs
{
    public static class Extensions
    {
        public static dynamic Resolve(this IServiceProvider provider, Type classType)
        {
            var assembly = Assembly.GetExecutingAssembly();

            foreach (var type in assembly.DefinedTypes)
            {
                if (type.ImplementedInterfaces.Contains(classType))
                    return ActivatorUtilities.CreateInstance(provider, type.AsType());
            }

            return null;
        }

        public static IServiceCollection RegisterCqrs(this IServiceCollection services)
        {
            services.AddScoped<IDispatcher, Dispatcher>();
            services.AddScoped<ICommandDispatcher, CommandDispatcher>();
            services.AddScoped<IQueryDispatcher, QueryDispatcher>();
            return services;
        }
    }
}
