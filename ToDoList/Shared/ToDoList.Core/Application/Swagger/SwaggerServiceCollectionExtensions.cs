using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace ToDoList.Core.Application.Swagger;

public static class SwaggerServiceCollectionExtensions
{
    public static void AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            using (var provider = services.BuildServiceProvider())
            {
                var versionDescriptionProvider = provider.GetRequiredService<IApiVersionDescriptionProvider>();

                foreach (var description in versionDescriptionProvider.ApiVersionDescriptions)
                    options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
            }
        });
    }

    private static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
    {
        var info = new OpenApiInfo
        {
            Title = $"ToDoList API {description.ApiVersion}",
            Version = description.ApiVersion.ToString(),

        };

        if (description.IsDeprecated)
        {
            info.Description += " This API version has been deprecated.";
        }

        return info;
    }
}
