using Core.Application.Activators;
using Core.Application.Dispatcher;
using Core.Domain.Entities;
using Core.Domain.Events;
using EventStore.ClientAPI;
using Florist.Infrastructure.EventStore;
using Florist.Infrastructure.EventStore.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Florist.Infrastructure.Persistence.EventStore
{
    public class EventStoreRepository : IRepository
    {
        private readonly IEventStoreContext _eventStoreContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventActivator _eventActivator;
        private readonly IDispatcher _dispatcher;

        public EventStoreRepository(IEventStoreContext eventStoreContext,
                                    IUnitOfWork unitOfWork,
                                    IEventActivator eventActivator,
                                    IDispatcher dispatcher)
        {
            _eventStoreContext = eventStoreContext;
            _unitOfWork = unitOfWork;
            _eventActivator = eventActivator;
            _dispatcher = dispatcher;
        }

        public async Task<T> GetByIdAsync<T>(Guid id) where T : IAggregate, new()
        {
            var events = await GetEvents<T>(id);
            var aggregate = new T();
            aggregate.Rehydrate(events);

            return aggregate;
        }

        public async Task SaveAsync<T>(T aggregate) where T : IAggregate
        {
            var newEvents = aggregate.GetUncommittedEvents();
            if (!newEvents.Any())
                return;

            var stream = GetStreamName<T>(aggregate.Id);

            await _unitOfWork.SaveChangesAsync(stream, newEvents);

            await PublishEvents(newEvents);

            aggregate.ClearCommittedEvents();
        }

        private async Task<IEnumerable<IEvent>> GetEvents<T>(Guid id)
        {
            var events = new List<IEvent>();
            var stream = GetStreamName<T>(id);

            var storeEvents = await GetStoreEvents(stream);
            var transactionEvents = _unitOfWork.GetEvents(stream);

            events.AddRange(storeEvents);
            events.AddRange(transactionEvents);

            return events;
        }

        private string GetStreamName<T>(Guid aggreagateId)
            => $"{typeof(T).Name}--{aggreagateId}";

        private async Task<IEnumerable<IEvent>> GetStoreEvents(string stream)
        {
            var events = new List<IEvent>();
            StreamEventsSlice currentSlice;
            long nextSliceStart = StreamPosition.Start;

            do
            {
                currentSlice = await _eventStoreContext.Connection.ReadStreamEventsForwardAsync(stream,
                                                                                                nextSliceStart,
                                                                                                200,
                                                                                                false);
                nextSliceStart = currentSlice.NextEventNumber;
                events.AddRange(currentSlice.Events.Select(x => x.DeserializeEvent()));
            } while (!currentSlice.IsEndOfStream);

            return events;
        }

        private async Task PublishEvents(IEnumerable<IEvent> events)
        {
            foreach (var @event in events)
            {
                var commandEvent = _eventActivator.CreateCommandEvent(@event);
                await _dispatcher.PublishAsync(commandEvent);
            }
        }
    }
}
