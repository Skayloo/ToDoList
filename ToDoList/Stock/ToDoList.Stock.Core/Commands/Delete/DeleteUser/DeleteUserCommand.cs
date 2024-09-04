using Newtonsoft.Json;
using ToDoList.Abstractions.Application.MediatR.Commands;

namespace ToDoList.Stock.Core.Commands.Delete.DeleteUser;

public class DeleteUserCommand : Command<DeleteUserCommandResult>
{
    [JsonIgnore]
    public int UserId { get; set; }
}
