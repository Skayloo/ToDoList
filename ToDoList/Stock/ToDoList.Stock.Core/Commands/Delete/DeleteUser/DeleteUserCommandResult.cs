using ToDoList.Abstractions.Application.MediatR.Commands;

namespace ToDoList.Stock.Core.Commands.Delete.DeleteUser;

public class DeleteUserCommandResult : CommandResult
{
    public DeleteUserCommandResult(int statusCode, string error) : base(statusCode, error) { }
}
