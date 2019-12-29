using Core.Domain.Events;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Florist.Infrastructure.EventStore.Persistence
{
    public class EventTransaction
    {
        private readonly ConcurrentDictionary<string, List<IEvent>> _aggregateEvents;

        public EventTransaction(long id)
        {
            Id = id;
            _aggregateEvents = new ConcurrentDictionary<string, List<IEvent>>();
        }

        public long Id { get; }

        public void AddEvents(string stream, IEnumerable<IEvent> events)
        {
            _aggregateEvents.AddOrUpdate(stream, events.ToList(), (stream, aggregateEvents) =>
            {
                aggregateEvents.AddRange(events);
                return aggregateEvents;
            });
        }

        public IEnumerable<IEvent> GetEvents(string stream)
        {
            _aggregateEvents.TryGetValue(stream, out var events);
            return events ?? new List<IEvent>();
        }
    }
}
