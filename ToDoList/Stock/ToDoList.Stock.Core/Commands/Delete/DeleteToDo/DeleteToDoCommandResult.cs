using ToDoList.Abstractions.Application.MediatR.Commands;

namespace ToDoList.Stock.Core.Commands.Delete.DeleteToDo;

public class DeleteToDoCommandResult : CommandResult
{
    public DeleteToDoCommandResult(int statusCode, string error) : base(statusCode, error) { }
}
