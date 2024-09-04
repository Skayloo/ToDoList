using ToDoList.Abstractions.Application.MediatR.Commands;

namespace ToDoList.Stock.Core.Commands.Post.AddToDo;

public class AddToDoCommandResult : CommandResult
{
    public AddToDoCommandResult(int statusCode, string error) : base(statusCode, error)
    {
    }
}
