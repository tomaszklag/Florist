using Core.Domain.Events;
using Florist.Infrastructure.EventStore.Models;
using System;
using System.Text;
using System.Text.Json;

namespace Core.Infrastructure.EventStore
{
    public static class Extensions
    {
        public static IEvent DeserializeEvent(this EventData resolvedEvent)
        {
            var type = Type.GetType(resolvedEvent.Event);
            var eventData = Encoding.UTF8.GetString(resolvedEvent.Data);

            return (IEvent)JsonSerializer.Deserialize(eventData, type);
        }

        public static EventData ToEventData(this IEvent @event, string stream)
        {
            var data = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(@event));
            var eventTypeName = @event.GetType().Name;

            return new EventData(stream, eventTypeName, data);
        }
    }
}
