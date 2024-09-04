using Asp.Versioning;
using Microsoft.Extensions.DependencyInjection;

namespace ToDoList.Core.Application.ApiVersioning;

public static class ApiVersioningServiceCollectionExtensions
{
    public static void AddVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(o =>
        {
            o.DefaultApiVersion = new ApiVersion(1, 0);
            o.AssumeDefaultVersionWhenUnspecified = true;
            o.ReportApiVersions = true;
        })
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;

            });
    }
}
