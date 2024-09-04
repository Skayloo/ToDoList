using ToDoList.Abstractions.Application.MediatR.Commands;
using ToDoList.Stock.Core.Commands.Get.AllToDoSByUserId;

namespace ToDoList.Stock.Core.Commands.Get.ToDoByUserId;

public class GetAllToDoSByUserIdCommand : Command<GetAllToDoSByUserIdCommandResult>
{
    public int? UserId { get; init; }

    public bool? IsCompleted { get; init; }

    public int? Priority { get; init; }
}
