using Core.Application.Activators;
using Core.Application.Command;
using Core.Application.Dispatcher;
using Core.Application.Event;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Florist.Infrastructure.Mvc
{
    public static class FrameworkBootstrap
    {
        public static void ConfigureAppilcationServices(this IServiceCollection services)
            => services.AddTransient<IHandlerActivator, HandlerActivator>()
                       .AddTransient<ICommandBus, CommandBus>()
                       .AddTransient<IEventBus, EventBus>()
                       .AddTransient<IDispatcher, Dispatcher>();

        public static void ConfigureErrorHandlerMiddleware(this IServiceCollection services)
            => services.AddScoped<ErrorHandlerMiddleware>();

        public static IApplicationBuilder UseErrorHandler(this IApplicationBuilder builder)
            => builder.UseMiddleware<ErrorHandlerMiddleware>();
    }
}
