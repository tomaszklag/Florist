using Florist.Core.Types;
using Florist.Core.Types.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace Florist.Infrastructure.Data
{
    public static class Extensions
    {
        public static IServiceCollection AddMssql(this IServiceCollection services)
        {
            IConfiguration configuration;

            using (var serviceProvider = services.BuildServiceProvider())
            {
                configuration = serviceProvider.GetService<IConfiguration>();
            }

            var databaseOptions = new DatabaseOptions();
            configuration.Bind("Database", databaseOptions);

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(databaseOptions.ConnectionString);
            });

            return services;
        }

        public static void BuildDatabase(this IApplicationBuilder builder)
        {
            using var serviceScope = builder.ApplicationServices.CreateScope();
            var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

            context.Database.Migrate();
            DbInitializer.Initialize(context);
        }

        public static IServiceCollection RegisterAllRepositories(this IServiceCollection services)
        {
            var infrastructureAssembly = Assembly.GetExecutingAssembly();
            var coreAssembly = Assembly.GetAssembly(typeof(IRepository));
            var repositoryTypes = coreAssembly.GetTypes()
                                              .Where(t => t.IsInterface &&
                                                          t.IsInherit(typeof(IRepository)));

            foreach (var repositoryType in repositoryTypes)
            {
                var repositoryImplementation = infrastructureAssembly.GetTypes()
                                                                     .Where(t => t.IsClass &&
                                                                                 t.IsInherit(repositoryType))
                                                                     .FirstOrDefault();
                if (repositoryImplementation is null)
                    throw new NullReferenceException($"Repository that implement Interface {nameof(repositoryType)} was not found.");

                services.AddTransient(repositoryType, repositoryImplementation);
            }

            return services;
        }

        private static bool IsInherit(this Type type, Type inheritedType)
            => type.GetInterfaces().Contains(inheritedType);
    }
}
