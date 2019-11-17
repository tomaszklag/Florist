using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;

namespace Florist.Infrastructure.Mvc
{
    public static class Extensions
    {
        public static IMvcBuilder AddCustomMvc(this IServiceCollection services)
            => services
                .AddControllers()
                .AddFluentValidation(fv =>
                {
                    fv.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                })
                .AddDefaultJsonOptions();

        private static IMvcBuilder AddDefaultJsonOptions(this IMvcBuilder builder)
            => builder.AddJsonOptions(o =>
                {
                    o.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                    o.JsonSerializerOptions.WriteIndented = true;
                });

        public static void AddErrorHandlerMiddleware(this IServiceCollection services)
            => services.AddScoped<ErrorHandlerMiddleware>();

        public static IApplicationBuilder UseErrorHandler(this IApplicationBuilder builder)
            => builder.UseMiddleware<ErrorHandlerMiddleware>();
    }
}
