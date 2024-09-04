using MediatR;
using ToDoList.Stock.Db;

namespace ToDoList.Stock.Core.Commands.Patch.UpdateToDo;

public class UpdateToDoCommandHandler : IRequestHandler<UpdateToDoCommand, UpdateToDoCommandResult>
{
    private readonly IStockUnitOfWork _unitOfWork;

    public UpdateToDoCommandHandler(IStockUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<UpdateToDoCommandResult> Handle(UpdateToDoCommand command, CancellationToken cancellationToken)
    {
        var todo = await _unitOfWork.ToDoListRepository.GetToDoById(command.Id);

        if (todo == null)
            return new UpdateToDoCommandResult(404, "ToDo not found");

        todo.DueDate = command.DueDate;
        todo.Description = command.Description;
        todo.Title = command.Title;
        todo.UserId = command.UserId;

        await _unitOfWork.ToDoListRepository.UpdateToDo(todo);

        await _unitOfWork.SaveEntitiesAsync(cancellationToken);

        return new UpdateToDoCommandResult(200, string.Empty);
    }
}
