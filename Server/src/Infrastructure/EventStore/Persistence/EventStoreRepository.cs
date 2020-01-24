using Core.Application.Activators;
using Core.Application.Dispatcher;
using Core.Domain.Entities;
using Core.Domain.Events;
using Florist.Infrastructure.EventStore;
using Florist.Infrastructure.EventStore.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Florist.Infrastructure.Persistence.EventStore
{
    public class EventStoreRepository : IRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDispatcher _dispatcher;
        private readonly IMongoDatabase _eventStoreContext;

        public EventStoreRepository(IUnitOfWork unitOfWork,
                                    IDispatcher dispatcher,
                                    IMongoDatabase eventStoreContext)
        {
            _unitOfWork = unitOfWork;
            _dispatcher = dispatcher;
            _eventStoreContext = eventStoreContext;
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

            _unitOfWork.SaveChanges(stream, newEvents);

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

            var eventLogsCollection = _eventStoreContext.GetCollection<EventData>("EventLog");
            using var cursor = await eventLogsCollection.Find(x => x.Stream == stream).ToCursorAsync();

            while (await cursor.MoveNextAsync())
            {
                foreach (var doc in cursor.Current)
                    events.Add(doc.DeserializeEvent());
            }

            return events;
        }

        private async Task PublishEvents(IEnumerable<IEvent> events)
        {
            foreach (var @event in events)
            {
                await _dispatcher.PublishAsync(@event);
            }
        }
    }
}
