using ToDoList.Abstractions.Application.Modules;
using ToDoList.Abstractions.Application.Settings;
using ToDoList.Core.Application;
using ToDoList.Core.Application.ApiVersioning;
using ToDoList.Core.Application.Autofac;
using ToDoList.Core.Application.Swagger;
using ToDoList.Core.Infrastructure.Database;
using ToDoList.Core.Infrastructure.Logging;

namespace ToDoList;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IWebHostEnvironment env)
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true)
            .AddEnvironmentVariables();
        Configuration = builder.Build();
    }

    public IServiceProvider ConfigureServices(IServiceCollection services)
    {
        services.AddCors(o => o.AddPolicy("AllowAll", builder =>
        {
            builder
                .SetIsOriginAllowed(x => _ = true)
                .AllowAnyHeader()
            .AllowCredentials()
            .AllowAnyMethod();
        }));

        services.Configure<DbConnectionSettings>(Configuration.GetSection("DbConnectionSettings"));

        services.AddSingleton<IModuleProvider, ModuleProvider>();

        services.AddVersioning();

        services.AddSwagger();

        services.AddControllers().AddNewtonsoftJson();

        var mvc = services
            .AddMvc(options => options.EnableEndpointRouting = false)
            .AddControllersAsServices();

        return mvc.AddAutofac();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        var loggerFactory = app.ApplicationServices.GetService<ILoggerFactory>();
        var logger = loggerFactory.CreateLogger<Startup>();

        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseCors("AllowAll"); // Ну у нас же тестовое задание. Надо зайти потыкать сваггер

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseMiddleware<LoggingMiddleware>();

        app.UseDbMigrations();

        app.UseMvc();

        app.UseSwagger();
    }
}