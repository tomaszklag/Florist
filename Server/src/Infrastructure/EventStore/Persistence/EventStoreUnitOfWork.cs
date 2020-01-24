using Core.Domain.Events;
using Florist.Infrastructure.EventStore.Models;
using MongoDB.Driver;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Florist.Infrastructure.EventStore.Persistence
{
    public class EventStoreUnitOfWork : IUnitOfWork
    {
        private readonly IMongoDatabase _eventStoreContext;
        private static IClientSessionHandle _mongoDbSession;
        private static ConcurrentDictionary<string, ConcurrentQueue<IEvent>> _transactions;

        public EventStoreUnitOfWork(IMongoDatabase eventStoreContext)
        {
            _eventStoreContext = eventStoreContext;
        }

        public IEnumerable<IEvent> GetEvents(string stream)
        {
            if (_transactions is null) return new List<IEvent>();

            _transactions.TryGetValue(stream, out var eventTransaction);
            return eventTransaction?.ToList() ?? new List<IEvent>();
        }

        public void SaveChanges(string stream, IEnumerable<IEvent> events)
        {
            if (_transactions is null)
                _transactions = new ConcurrentDictionary<string, ConcurrentQueue<IEvent>>();

            if (_transactions.TryGetValue(stream, out var transaction))
                events?.ToList().ForEach(e => transaction.Enqueue(e));
            else
                _transactions.TryAdd(stream, new ConcurrentQueue<IEvent>(events));
        }

        public async Task CommitAsync()
        {
            _mongoDbSession = await _eventStoreContext.Client.StartSessionAsync();
            _mongoDbSession.StartTransaction();

            var insertToESTasks = new List<Task>();

            foreach (var eventTransaction in _transactions)
            {
                insertToESTasks.Add(InsertEventsToESAsync(eventTransaction.Value, eventTransaction.Key));
            }

            await Task.WhenAll(insertToESTasks);
            await _mongoDbSession.CommitTransactionAsync();
            _mongoDbSession.Dispose();
        }

        public async Task RollbackAsync()
        {
            await _mongoDbSession.AbortTransactionAsync();
            _mongoDbSession.Dispose();
        }

        private async Task InsertEventsToESAsync(IEnumerable<IEvent> events, string stream)
        {
            var eventDatas = new List<EventData>();
            var eventDataQueue = new Queue<EventData>(events.Count());
            events.ToList().ForEach(x => eventDataQueue.Enqueue(x.ToEventData(stream)));
            await _eventStoreContext.GetCollection<EventData>("EventLog").InsertManyAsync(eventDataQueue);
        }
    }
}
