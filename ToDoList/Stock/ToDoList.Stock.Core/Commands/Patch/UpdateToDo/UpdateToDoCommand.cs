using System.Text.Json.Serialization;
using ToDoList.Abstractions.Application.MediatR.Commands;
using ToDoList.Stock.Db.Domain;

namespace ToDoList.Stock.Core.Commands.Patch.UpdateToDo;

public class UpdateToDoCommand : Command<UpdateToDoCommandResult>
{
    [JsonIgnore]
    public int Id { get; set; }

    public string Title { get; init; }

    public string Description { get; init; }

    public bool? IsCompleted { get; init; }

    public int UserId { get; init; }

    public DateTime? DueDate { get; init; }

    public virtual Priority Priority { get; init; }
}
