using Newtonsoft.Json;
using ToDoList.Abstractions.Application.MediatR.Commands;

namespace ToDoList.Stock.Core.Commands.Patch.UpdateUser;

public class UpdateUserCommand : Command<UpdateUserCommandResult>
{
    [JsonIgnore]
    public int UserId { get; set; }
    
    public string Name { get; init; }
}
