using ToDoList.Abstractions.Application.MediatR.Commands;

namespace ToDoList.Stock.Core.Commands.Patch.UpdateToDo;

public class UpdateToDoCommandResult : CommandResult
{
    public UpdateToDoCommandResult(int statusCode, string error) : base(statusCode, error)
    {
    }
}
