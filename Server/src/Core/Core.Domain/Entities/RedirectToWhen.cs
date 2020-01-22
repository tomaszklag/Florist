using Core.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Core.Domain.Entities
{
    public static class RedirectToWhen
    {
        private static class Cache<T>
        {
            public static readonly IDictionary<Type, MethodInfo> Store = typeof(T)
                .GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(m => m.Name == "When")
                .Where(m => m.GetParameters().Length == 1 && 
                            m.GetParameters().First().ParameterType == typeof(IEvent))
                .ToDictionary(m => m.GetParameters().First().ParameterType, m => m);
        }

        public static void InvokeEvent<T>(T instance, IEvent @event)
        {
            var eventType = @event.GetType();
            var genericCache = typeof(Cache<>).MakeGenericType(instance.GetType()).GetField("Store");
            if (!Cache<T>.Store.TryGetValue(eventType, out var whenMethodInfo))
                throw new InvalidOperationException($"Failed to locate method: {typeof(T).Name}.When({eventType.Name})");

            whenMethodInfo.Invoke(instance, new[] { @event });
        }
    }
}
