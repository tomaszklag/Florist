using Core.Domain.Events;
using System;
using System.Collections.Generic;

namespace Core.Domain.Entities
{
    public abstract class Aggregate : IAggregate
    {
        public Guid Id { get; protected set; }
        public int Version { get; protected set; }

        private readonly IList<IEvent> _uncommittedEvents = new List<IEvent>();

        public IEnumerable<IEvent> GetUncommittedEvents() => _uncommittedEvents;

        public void ClearCommittedEvents()
        {
            _uncommittedEvents.Clear();
        }

        public void Rehydrate(IEnumerable<IEvent> events)
        {
            foreach (var @event in events)
            {
                Apply(@event);
            }
        }

        protected void Publish(IEvent @event)
        {
            _uncommittedEvents.Add(@event);
            Apply(@event);
        }

        private void Apply(IEvent @event)
        {
            Version++;
            RedirectToWhen.InvokeEvent(this, @event);
        }
    }
}
