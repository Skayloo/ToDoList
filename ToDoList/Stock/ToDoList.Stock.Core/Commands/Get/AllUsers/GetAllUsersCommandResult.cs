using ToDoList.Abstractions.Application.MediatR.Commands;
using ToDoList.Stock.Db.Domain;

namespace ToDoList.Stock.Core.Commands.Get.AllUsers;

public class GetAllUsersCommandResult : CommandResult<IEnumerable<User>>
{
    public GetAllUsersCommandResult(IEnumerable<User> result) : base(result)
    {
    }

    public GetAllUsersCommandResult(int statusCode, string error) : base(statusCode, error)
    {
    }
}
