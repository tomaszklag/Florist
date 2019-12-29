using System;
using System.Collections.Generic;
using System.Text;
using EventStore.ClientAPI;
using Florist.Infrastructure.EventStore.Configurations;
using Microsoft.Extensions.Options;

namespace Florist.Infrastructure.EventStore.Persistence
{
    public class EventStoreContext : IEventStoreContext
    {
        public EventStoreContext(IOptions<EventStoreConfiguration> options)
        {
            var config = options.Value;
            var endpoint = new Uri($"tcp://{config.Username}:{config.Password}@{config.Address}:{config.Port}");
            Connection = EventStoreConnection.Create(ConnectionSettings.Default, endpoint);
            Connection.ConnectAsync().Wait();
        }

        public IEventStoreConnection Connection { get; }
    }
}
