using Autofac;
using Microsoft.AspNetCore.Http;

namespace ToDoList.Core.Application.Autofac.Modules;

internal class SharedModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>().SingleInstance();
    }
}
