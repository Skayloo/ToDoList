using Newtonsoft.Json;
using ToDoList.Abstractions.Application.MediatR.Commands;

namespace ToDoList.Stock.Core.Commands.Delete.DeleteToDo;

public class DeleteToDoCommand : Command<DeleteToDoCommandResult>
{
    [JsonIgnore]
    public int Id { get; set; }
}
