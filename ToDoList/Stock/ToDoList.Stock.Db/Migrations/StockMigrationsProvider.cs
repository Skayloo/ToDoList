using Microsoft.Extensions.Logging;
using ToDoList.Db.Abstractions.Migrations;
using ToDoList.Stock.Db.Context;

namespace ToDoList.Stock.Db.Migrations;

public class StockMigrationsProvider : DbMigrationsProvider<StockContext>
{
    public StockMigrationsProvider(StockContext context, ILoggerFactory loggerFactory) : base(context, loggerFactory)
    {
    }

    public override int Index => 1;
}
