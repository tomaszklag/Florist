using Core.Domain.Events;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Florist.Infrastructure.EventStore
{
    public interface IUnitOfWork
    {
        IEnumerable<IEvent> GetEvents(string stream);
        Task SaveChangesAsync(string stream, IEnumerable<IEvent> events);
        Task CommitAsync();
        void Rollback();
    }
}
