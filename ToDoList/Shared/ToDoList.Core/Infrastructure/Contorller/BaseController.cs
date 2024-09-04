using Microsoft.AspNetCore.Mvc;
using ToDoList.Abstractions.Application.MediatR.Commands;

namespace ToDoList.Core.Infrastructure.Contorller;

public class BaseController : ControllerBase
{
    protected IActionResult OkCommandResult<T>(CommandResult<T> commandResult)
    {
        return commandResult.StatusCode == 200 ? Ok(commandResult.Result) :
            StatusCode(commandResult.StatusCode, commandResult.Error);
    }

    protected IActionResult OkCommandResult(CommandResult commandResult)
    {
        return commandResult.StatusCode == 200 ?
            Ok(new { Success = true }) :
            StatusCode(commandResult.StatusCode, commandResult.Error);
    }
}
