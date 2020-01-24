namespace Florist.Infrastructure.EventStore.Configurations
{
    public class EventStoreConfiguration
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string EventLogCollectionName { get; set; }
    }
}
