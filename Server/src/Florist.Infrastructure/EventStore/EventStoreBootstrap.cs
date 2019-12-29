using Core.Domain.Entities;
using Florist.Infrastructure.EventStore.Configurations;
using Florist.Infrastructure.EventStore.Persistence;
using Florist.Infrastructure.Persistence.EventStore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Florist.Infrastructure.EventStore
{
    public static class EventStoreBootstrap
    {
        public static void ConfigureEventStore(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EventStoreConfiguration>(cfg =>
            {
                cfg.Username = configuration.GetSection("EventStoreOptions:Username").Value;
                cfg.Password = configuration.GetSection("EventStoreOptions:Password").Value;
                cfg.Address = configuration.GetSection("EventStoreOptions:Address").Value;
                cfg.Port = configuration.GetSection("EventStoreOptions:Port").Value;
            });

            services.AddSingleton<IEventStoreContext, EventStoreContext>();
            //services.AddScoped<IUnitOfWork, EventStoreUnitOfWork>();
            services.AddTransient<IRepository, EventStoreRepository>();
            //services.AddTransient<IStreamHandler, StreamHandler>();
            //services.AddTransient<IEventStoreListener, EventStoreListener>();
        }
    }
}
