using Florist.Services.Dispatchers;
using Microsoft.Extensions.DependencyInjection;

namespace Florist.Services
{
    public static class ServicesRegister
    {
        public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IDispatcher, Dispatcher>();
            services.AddScoped<ICommandBus, CommandBus>();
            services.AddScoped<IQueryBus, QueryBus>();
            return services;
        }
    }
}
