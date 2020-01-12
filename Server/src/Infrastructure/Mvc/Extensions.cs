using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Florist.Infrastructure.Mvc
{
    public static class Extensions
    {
        public static void AddErrorHandlerMiddleware(this IServiceCollection services)
            => services.AddScoped<ErrorHandlerMiddleware>();

        public static IApplicationBuilder UseErrorHandler(this IApplicationBuilder builder)
            => builder.UseMiddleware<ErrorHandlerMiddleware>();
    }
}
