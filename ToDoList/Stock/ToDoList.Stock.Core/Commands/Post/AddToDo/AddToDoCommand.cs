using ToDoList.Abstractions.Application.MediatR.Commands;
using ToDoList.Stock.Db.Domain;

namespace ToDoList.Stock.Core.Commands.Post.AddToDo;

public class AddToDoCommand : Command<AddToDoCommandResult>
{ 
    public string Title { get; init; }

    public string Description { get; init; }

    public bool? IsCompleted { get; init; }

    public DateTime? DueDate { get; init; }

    public int UserId { get; init; }

    public int PriorityLevel { get; init; }
}
