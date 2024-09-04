using ToDoList.Abstractions.Application.MediatR.Commands;
using ToDoList.Stock.Db.Domain;

namespace ToDoList.Stock.Core.Commands.Get.AllToDoSByUserId;

public class GetAllToDoSByUserIdCommandResult : CommandResult<IEnumerable<ToDoItem>>
{
    public GetAllToDoSByUserIdCommandResult(IEnumerable<ToDoItem> result) : base(result)
    {
    }

    public GetAllToDoSByUserIdCommandResult(int statusCode, string error) : base(statusCode, error)
    {
    }
}
