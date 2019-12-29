using Core.Domain.Events;
using EventStore.ClientAPI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Florist.Infrastructure.EventStore
{
    public static class Extensions
    {
        public static IEvent DeserializeEvent(this ResolvedEvent resolvedEvent)
        {
            if (resolvedEvent.OriginalStreamId.StartsWith('$'))
                return null;

            var eventMetadata = Encoding.UTF8.GetString(resolvedEvent.OriginalEvent.Metadata);
            var eventTypeName = JsonSerializer.Deserialize<dynamic>(eventMetadata)["Type"];
            var type = Type.GetType(eventTypeName);
            var eventData = Encoding.UTF8.GetString(resolvedEvent.OriginalEvent.Data);

            return (IEvent)JsonSerializer.Deserialize(eventData, type);
        }

        public static EventData ToEventData(this IEvent @event)
        {
            var data = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(@event));
            var header = new Dictionary<string, object>()
            {
                { "Type", @event.GetType().AssemblyQualifiedName }
            };
            var metadata = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(header));
            var typeName = @event.GetType().Name;
            var eventId = Guid.NewGuid();

            return new EventData(eventId, typeName, true, data, metadata);
        }
    }
}
