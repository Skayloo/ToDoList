using ToDoList.Abstractions.Application.MediatR.Commands;

namespace ToDoList.Stock.Core.Commands.Post.AddUser;

public class AddUserCommandResult : CommandResult
{
    public AddUserCommandResult(int statusCode, string error) : base(statusCode, error)
    {
    }
}
