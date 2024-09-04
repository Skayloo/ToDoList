using Autofac;
using FluentValidation;
using MediatR;
using ToDoList.Core.Infrastructure.Contorller;
using ToDoList.Db.Abstractions.Context;
using ToDoList.Db.Abstractions.Migrations;
using ToDoList.Stock.Core;
using ToDoList.Stock.Db;
using ToDoList.Stock.Db.Context;
using ToDoList.Stock.Db.Migrations;

[assembly: ToDoList.Abstractions.Application.Modules.Module(typeof(MainModule))]
namespace ToDoList.Stock.Core;

public class MainModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.AddDbContext<StockContext>();
        builder.RegisterType<StockMigrationsProvider>().As<IMigrationsProvider>().InstancePerLifetimeScope();
        builder.RegisterType<StockUnitOfWork>().As<IStockUnitOfWork>().InstancePerLifetimeScope();

        builder.RegisterAssemblyTypes(ThisAssembly).AsClosedTypesOf(typeof(IRequestHandler<,>));
        builder.RegisterAssemblyTypes(ThisAssembly).AsClosedTypesOf(typeof(INotificationHandler<>));

        builder.RegisterAssemblyTypes(ThisAssembly)
            .Where(t => t.IsClosedTypeOf(typeof(IValidator<>)))
            .AsImplementedInterfaces();

        builder.RegisterAssemblyTypes(ThisAssembly)
            .Where(t => t.IsSubclassOf(typeof(BaseController)))
            .PropertiesAutowired();
    }
}
