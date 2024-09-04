using MediatR;
using ToDoList.Stock.Db;

namespace ToDoList.Stock.Core.Commands.Delete.DeleteToDo;

public class DeleteToDoCommandHandler : IRequestHandler<DeleteToDoCommand, DeleteToDoCommandResult>
{
    private readonly IStockUnitOfWork _unitOfWork;

    public DeleteToDoCommandHandler(IStockUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<DeleteToDoCommandResult> Handle(DeleteToDoCommand command, CancellationToken cancellationToken)
    {
        var todo = await _unitOfWork.ToDoListRepository.GetToDoById(command.Id);

        if (todo == null)
            return new DeleteToDoCommandResult(404, "User not found");

        todo.IsDeleted = true;

        await _unitOfWork.ToDoListRepository.UpdateToDo(todo);

        await _unitOfWork.SaveEntitiesAsync(cancellationToken);

        return new DeleteToDoCommandResult(200, string.Empty);
    }
}
