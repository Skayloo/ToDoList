using ToDoList.Abstractions.Application.MediatR.Commands;

namespace ToDoList.Stock.Core.Commands.Post.AddUser;

public class AddUserCommand : Command<AddUserCommandResult>
{
    public string Name { get; init; }
}
