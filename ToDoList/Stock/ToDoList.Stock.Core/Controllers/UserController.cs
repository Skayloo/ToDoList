using MediatR;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Core.Infrastructure.Contorller;
using ToDoList.Stock.Core.Commands.Delete.DeleteUser;
using ToDoList.Stock.Core.Commands.Get.AllUsers;
using ToDoList.Stock.Core.Commands.Patch.UpdateUser;
using ToDoList.Stock.Core.Commands.Post.AddUser;
using ToDoList.Stock.Db.Domain;

namespace ToDoList.Stock.Core.Controllers;

[Produces("application/json")]
[Route("api/users")]
[ApiController]
public class UserController : BaseController
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet]
    [Route("")]
    [ProducesResponseType(typeof(IEnumerable<User>), 200)]
    public async Task<IActionResult> GetAllUsers()
    {
        var res = await _mediator.Send(new GetAllUsersCommand());
        return OkCommandResult(res);
    }

    [HttpPost]
    [Route("")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> AddUsers([FromBody] AddUserCommand command)
    {
        var res = await _mediator.Send(command);
        return OkCommandResult(res);
    }

    [HttpPatch]
    [Route("{id:int}")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> UpdateUser([FromRoute] int id, [FromBody] UpdateUserCommand command)
    {
        command.UserId = id;
        var res = await _mediator.Send(command);
        return OkCommandResult(res);
    }

    [HttpDelete]
    [Route("{id:int}")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> DeleteUser([FromRoute] int id)
    {
        var res = await _mediator.Send(new DeleteUserCommand()
        {
            UserId = id,
        });
        return OkCommandResult(res);
    }
}