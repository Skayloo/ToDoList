using System.Reflection;

namespace ToDoList.Abstractions.Application.Modules;

public interface IModuleProvider
{
    IEnumerable<Assembly> ApplicationPartAssemblies { get; }
}
