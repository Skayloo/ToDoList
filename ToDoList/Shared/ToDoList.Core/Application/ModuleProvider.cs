using Microsoft.Extensions.DependencyModel;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Reflection;
using System.Runtime.Loader;
using ToDoList.Abstractions.Application.Modules;
using ToDoList.Abstractions.Application.Settings;

namespace ToDoList.Core.Application;

public class ModuleProvider : IModuleProvider
{
    private readonly IOptions<DbConnectionSettings> _settings;
    private readonly ILogger _logger;
    private readonly IList<Assembly> _applicationPartAssemblies;

    public ModuleProvider(IOptions<DbConnectionSettings> settings, ILoggerFactory loggerFactory)
    {
        _settings = settings;
        _applicationPartAssemblies = new List<Assembly>();
        _logger = loggerFactory.CreateLogger<ModuleProvider>();

        InitModuleAssemblies();
    }

    private void InitModuleAssemblies()
    {
        var files = GetAllFilesFromModuleDirectory();
        foreach (var file in files)
        {
            var assembly = AssemblyLoad(file);
            if (assembly == null) continue;
            if (assembly.GetCustomAttribute<ModuleAttribute>() != null)
            {
                _applicationPartAssemblies.Add(assembly);
            }
        }
    }

    private IEnumerable<FileSystemInfo> GetAllFilesFromModuleDirectory()
    {
        var modulePath = _settings?.Value.ModulePath;
        if (string.IsNullOrEmpty(modulePath))
            throw new ArgumentNullException($"ModulePath", "ModulePath undefined");

        var moduleRootFolder = new DirectoryInfo(modulePath);
        var moduleFolders = moduleRootFolder.GetDirectories();
        foreach (var moduleFolder in moduleFolders)
        {
            foreach (var file in moduleFolder.GetFileSystemInfos("*.dll", SearchOption.AllDirectories))
            {
                yield return file;
            }
        }
    }

    private Assembly AssemblyLoad(FileSystemInfo file)
    {
        Assembly assembly = null;
        try
        {
            var fileNameWithOutExtension = Path.GetFileNameWithoutExtension(file.FullName);
            var inCompileLibraries = DependencyContext.Default.CompileLibraries.Any(l =>

                l.Name.Equals(fileNameWithOutExtension, StringComparison.OrdinalIgnoreCase));
            var inRuntimeLibraries = DependencyContext.Default.RuntimeLibraries.Any(l =>
                l.Name.Equals(fileNameWithOutExtension, StringComparison.OrdinalIgnoreCase));


            assembly = (inCompileLibraries || inRuntimeLibraries)
                ? Assembly.Load(new AssemblyName(fileNameWithOutExtension))
                : AssemblyLoadContext.Default.LoadFromAssemblyPath(file.FullName);
        }
        catch (FileLoadException exception) when (!exception.Message.Contains(
            "Assembly with same name is already loaded"))
        {
            _logger.LogCritical(exception, "ModuleProvider can't load {Assembly}", file.FullName);
        }

        return assembly;
    }

    public IEnumerable<Assembly> ApplicationPartAssemblies => _applicationPartAssemblies.ToArray();
}  
