using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ToDoList.Db.Abstractions.Migrations;

namespace ToDoList.Core.Infrastructure.Database;

public static class DbApplicationBuilderExtensions
{
    public static void UseDbMigrations(this IApplicationBuilder app)
    {
        var factory = app.ApplicationServices.GetRequiredService<ILoggerFactory>();
        var logger = factory.CreateLogger("DB");

        try
        {
            var providers = app.ApplicationServices.GetServices<IMigrationsProvider>();
            foreach (var provider in providers.OrderBy(p => p.Index))
            {
                if (provider.Migrate())
                {
                    logger.LogInformation("DB - {Migration} {Module}", "Scheme updated for ", provider.GetType().Assembly.GetName().Name);
                }
                else
                {
                    logger.LogCritical("DB -  {Migration} {Module}", "Scheme updated error for ", provider.GetType().Assembly.GetName().Name);
                }
            }
        }
        catch (Exception exception)
        {
            logger.LogCritical(exception, "DB - {Migration}", "Not DbInitialize");
        }
    }
}
