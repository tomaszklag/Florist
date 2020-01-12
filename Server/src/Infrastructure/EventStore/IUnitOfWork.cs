using Core.Domain.Events;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Florist.Infrastructure.EventStore
{
    public interface IUnitOfWork
    {
        IEnumerable<IEvent> GetEvents(string stream);
        void SaveChanges(string stream, IEnumerable<IEvent> events);
        Task CommitAsync();
        Task RollbackAsync();
    }
}
