namespace ToDoList.Abstractions.Application.Modules;

[AttributeUsage(AttributeTargets.Assembly)]
public class ModuleAttribute : Attribute
{
    public ModuleAttribute(Type moduleType = null)
    {
        ModuleType = moduleType;
    }

    public Type ModuleType { get; }
}
