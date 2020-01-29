namespace Core.Infrastructure.Swagger
{
    public static class Extensions
    {
        //public static IServiceCollection AddSwagger(this IServiceCollection services)
        //    => services.AddSwaggerGen(c =>
        //        {
        //            c.SwaggerDoc("v1", new OpenApiInfo()
        //            {
        //                Title = "Florist App",
        //                Version = "1.0.0"
        //            });

        //            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        //            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        //            c.IncludeXmlComments(xmlPath);
        //        });

        //public static IApplicationBuilder UseSwagger(this IApplicationBuilder builder)
        //{
        //    builder.UseSwagger();
        //    builder.UseSwaggerUI(c =>
        //    {
        //        c.SwaggerEndpoint("/swagger/swagger.json", "Florsit Api version 1.0.0");
        //    });

        //    return builder;
        //}
    }
}
