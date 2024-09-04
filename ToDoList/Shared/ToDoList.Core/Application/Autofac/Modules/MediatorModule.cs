using Autofac;
using Autofac.Extensions.DependencyInjection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using ToDoList.Abstractions.Application.MediatR.Behaviors;
using Module = Autofac.Module;

namespace ToDoList.Core.Application.Autofac.Modules;

public class MediatorModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
            .AsImplementedInterfaces();

        builder.RegisterGeneric(typeof(ValidatorBehavior<,>)).As(typeof(IPipelineBehavior<,>));

        var services = new ServiceCollection();
        builder.Populate(services);
    }
}
