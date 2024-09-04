using Microsoft.Extensions.DependencyInjection;

namespace ToDoList.Core.Application.Autofac;

public static class AutofacServiceCollectionExtensions
{
    public static IServiceProvider AddAutofac(this IMvcBuilder mvcBuilder)
    {
        var builder = new AutofacBuilder(mvcBuilder);
        return builder.GetServiceProvider();
    }
}
