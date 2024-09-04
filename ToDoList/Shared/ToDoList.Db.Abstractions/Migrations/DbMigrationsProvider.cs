using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ToDoList.Db.Abstractions.Context;

namespace ToDoList.Db.Abstractions.Migrations;

public abstract class DbMigrationsProvider<T> : IMigrationsProvider where T : DbContextBase
{
    private readonly T _context;
    private readonly ILogger _logger;

    public DbMigrationsProvider(T context, ILoggerFactory loggerFactory)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        if (loggerFactory == null) throw new ArgumentNullException(nameof(loggerFactory));

        _logger = loggerFactory.CreateLogger<T>();
    }

    public abstract int Index { get; }

    public virtual bool Migrate()
    {
        try
        {
            _context.Database.Migrate();
            return true;
        }
        catch (Exception exception)
        {
            _logger.LogCritical(exception, "DB - {CONTEXT MIGRATION} error", _context.GetType().FullName);
        }

        return false;
    }
}