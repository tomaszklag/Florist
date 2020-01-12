using Core.Domain.Entities;
using Florist.Infrastructure.EventStore.Configurations;
using Florist.Infrastructure.EventStore.Persistence;
using Florist.Infrastructure.Persistence.EventStore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Florist.Infrastructure.EventStore
{
    public static class EventStoreBootstrap
    {
        public static void ConfigureEventStore(this IServiceCollection services)
        {
            var esConfig = GetESConfiguration(services);
            var client = new MongoClient(esConfig.ConnectionString);
            var database = client.GetDatabase(esConfig.DatabaseName);

            database.CreateCollection("EventLog");

            services.AddSingleton(database);
            services.AddTransient<IRepository, EventStoreRepository>();
            services.AddScoped<IUnitOfWork, EventStoreUnitOfWork>();
        }

        private static EventStoreConfiguration GetESConfiguration(IServiceCollection services)
        {
            using var serviceProvider = services.BuildServiceProvider();

            var configuration = serviceProvider.GetService<IConfiguration>();
            var esConfig = new EventStoreConfiguration();

            configuration.Bind("EventStore", esConfig);

            return esConfig;
        }
    }
}
