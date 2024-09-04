using MediatR;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Core.Infrastructure.Contorller;
using ToDoList.Stock.Core.Commands.Delete.DeleteToDo;
using ToDoList.Stock.Core.Commands.Get.ToDoByUserId;
using ToDoList.Stock.Core.Commands.Patch.UpdateToDo;
using ToDoList.Stock.Core.Commands.Post.AddToDo;
using ToDoList.Stock.Db.Domain;

namespace ToDoList.Stock.Core.Controllers;


[Produces("application/json")]
[Route("api/todo")]
[ApiController]
public class ToDoListController : BaseController
{
    private readonly IMediator _mediator;

    public ToDoListController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet]
    [Route("")]
    [ProducesResponseType(typeof(IEnumerable<ToDoItem>), 200)]
    public async Task<IActionResult> GetTodosByUserId([FromQuery] GetAllToDoSByUserIdCommand command)
    {
        var res = await _mediator.Send(command);
        return OkCommandResult(res);
    }

    [HttpPost]
    [Route("")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> AddToDo([FromBody] AddToDoCommand command)
    {
        var res = await _mediator.Send(command);
        return OkCommandResult(res);
    }

    [HttpPatch]
    [Route("{id:int}")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> UpdateToDo([FromRoute] int id, [FromBody] UpdateToDoCommand command)
    {
        command.Id = id;
        var res = await _mediator.Send(command);
        return OkCommandResult(res);
    }

    [HttpDelete]
    [Route("{id:int}")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> DeleteToDo([FromRoute] int id)
    {
        var res = await _mediator.Send(new DeleteToDoCommand()
        {
            Id = id,
        });
        return OkCommandResult(res);
    }
}

