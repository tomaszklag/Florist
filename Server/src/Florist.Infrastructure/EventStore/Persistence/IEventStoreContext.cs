using EventStore.ClientAPI;

namespace Florist.Infrastructure.EventStore.Persistence
{
    public interface IEventStoreContext
    {
        IEventStoreConnection Connection { get; }
    }
}
