using Autofac;
using Autofac.Core;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Reflection;
using ToDoList.Abstractions.Application.Modules;
using Module = Autofac.Module;

namespace ToDoList.Core.Application.Autofac.Modules;

public class ApplicationPartModule : Module
{
    private readonly IModuleProvider _moduleProvider;
    private readonly IMvcBuilder _mvcBuilder;
    private readonly ILogger _logger;

    public ApplicationPartModule(IMvcBuilder mvcBuilder, IModuleProvider moduleProvider, ILoggerFactory loggerFactory)
    {
        _mvcBuilder = mvcBuilder;
        _moduleProvider = moduleProvider;
        _logger = loggerFactory.CreateLogger<ApplicationPartModule>();
    }

    protected override void Load(ContainerBuilder builder)
    {
        foreach (var assembly in _moduleProvider.ApplicationPartAssemblies)
        {
            _logger.LogInformation("ModuleProvider - {Module} {Message}", assembly.GetName().Name, "initialize starting...");

            _mvcBuilder.PartManager.ApplicationParts.Add(new AssemblyPart(assembly));

            var atrribute = assembly.GetCustomAttribute<ModuleAttribute>();
            var moduleStartup = (IModule)Activator.CreateInstance(atrribute.ModuleType);
            builder.RegisterModule(moduleStartup);

            _logger.LogInformation("ModuleProvider - {Module} {Message}", assembly.GetName().Name, "loading complited");
        }
    }
}