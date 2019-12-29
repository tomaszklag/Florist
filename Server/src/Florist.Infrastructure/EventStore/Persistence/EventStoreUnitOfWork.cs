using Core.Domain.Events;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Florist.Infrastructure.EventStore.Persistence
{
    public class EventStoreUnitOfWork : IUnitOfWork
    {
        private readonly IEventStoreContext _eventStoreContext;
        private readonly ConcurrentDictionary<string, EventTransaction> _transactions;

        public EventStoreUnitOfWork(IEventStoreContext eventStoreContext)
        {
            _eventStoreContext = eventStoreContext;
            _transactions = new ConcurrentDictionary<string, EventTransaction>();
        }

        public IEnumerable<IEvent> GetEvents(string stream)
        {
            _transactions.TryGetValue(stream, out var eventTransaction);
            return eventTransaction?.GetEvents(stream) ?? new List<IEvent>();
        }

        public async Task SaveChangesAsync(string stream, IEnumerable<IEvent> events)
        {
            var transaction = await GetTransactionAsync(stream);
            var transactionEvents = events.Select(x => x.ToEventData());
            await transaction.WriteAsync(transactionEvents);

            if (_transactions.TryGetValue(stream, out var eventTransaction))
                eventTransaction.AddEvents(stream, events);
        }

        public Task CommitAsync()
        {
            throw new NotImplementedException();
        }

        public void Rollback()
        {
            throw new NotImplementedException();
        }

        private async Task<EventStoreTransaction> GetTransactionAsync(string stream)
        {
            var transaction = _transactions.TryGetValue(stream, out var tx)
                ? _eventStoreContext.Connection.ContinueTransaction(tx.Id)
                : await CreateTransactionAsync(stream);

            return transaction;
        }

        private async Task<EventStoreTransaction> CreateTransactionAsync(string stream)
        {
            var transaction = await _eventStoreContext.Connection.StartTransactionAsync(stream, ExpectedVersion.Any);
            _transactions.TryAdd(stream, new EventTransaction(transaction.TransactionId));
            return transaction;
        }
    }
}
