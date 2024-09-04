using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ToDoList.Abstractions.Application.Settings;
using ToDoList.Db.Abstractions.Context;
using ToDoList.Stock.Db.Context.EntityConfigurations;

namespace ToDoList.Stock.Db.Context;

public class StockContext : DbContextBase
{
    public StockContext(IOptions<DbConnectionSettings> appSettings, IMediator mediator) : base(appSettings, mediator)
    {
    }

    protected override string MigrationsAssembly => GetType().Assembly.FullName;
    protected override string MigrationsHistoryTable => "migrations_histories";
    protected override string MigrationsSchema => "public";

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseSerialColumns();
        modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ToDoItemEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new PriorityEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new UsersOfToDoItemsEntityTypeConfiguration());
    }
}
