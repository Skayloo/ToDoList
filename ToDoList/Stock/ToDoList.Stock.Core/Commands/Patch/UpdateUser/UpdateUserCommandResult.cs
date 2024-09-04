using ToDoList.Abstractions.Application.MediatR.Commands;

namespace ToDoList.Stock.Core.Commands.Patch.UpdateUser;

public class UpdateUserCommandResult : CommandResult
{
    public UpdateUserCommandResult(int statusCode, string error) : base(statusCode, error)
    {
    }
}
