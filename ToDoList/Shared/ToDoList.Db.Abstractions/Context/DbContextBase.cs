using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ToDoList.Abstractions.Application.MediatR;
using ToDoList.Abstractions.Application.Settings;
using ToDoList.Domain.Abstractions;

namespace ToDoList.Db.Abstractions.Context;

public abstract class DbContextBase : DbContext
{
    private readonly IOptions<DbConnectionSettings> _appSettings;
    private readonly IMediator _mediator;

    protected abstract string MigrationsAssembly { get; }
    protected abstract string MigrationsHistoryTable { get; }
    protected abstract string MigrationsSchema { get; }

    protected DbContextBase(IOptions<DbConnectionSettings> appSettings, IMediator mediator)
    {
        _appSettings = appSettings;
        _mediator = mediator;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_appSettings.Value.DbConnection, builder =>
        {
            builder.MigrationsAssembly(MigrationsAssembly);
            builder.MigrationsHistoryTable(MigrationsHistoryTable, MigrationsSchema);
        });
    }

    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
    {
        await SaveChangesAsync();
        return true;
    }
}