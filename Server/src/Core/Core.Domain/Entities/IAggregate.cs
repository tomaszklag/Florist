using Core.Domain.Events;
using System.Collections.Generic;

namespace Core.Domain.Entities
{
    public interface IAggregate : IEntity
    {
        IEnumerable<IEvent> GetUncommittedEvents();
        void Rehydrate(IEnumerable<IEvent> events);
        void ClearCommittedEvents();
    }
}
