using MediatR;
using ToDoList.Stock.Db;
using ToDoList.Stock.Db.Domain;

namespace ToDoList.Stock.Core.Commands.Post.AddToDo;

public class AddToDoCommandHandler : IRequestHandler<AddToDoCommand, AddToDoCommandResult>
{
    private readonly IStockUnitOfWork _unitOfWork;

    public AddToDoCommandHandler(IStockUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<AddToDoCommandResult> Handle(AddToDoCommand command, CancellationToken cancellationToken)
    {
        var todo = await _unitOfWork.ToDoListRepository.AddToDo(new ToDoItem()
        {
            Title = command.Title,
            Description = command.Description,
            IsCompleted = command.IsCompleted,
            DueDate = command.DueDate,
            UserId = command.UserId,
            PriorityLevel = command.PriorityLevel
        });

        await _unitOfWork.SaveEntitiesAsync(cancellationToken);

        await _unitOfWork.UserOfToDoItemsRepository.AddConnection(new UsersOfToDoItems()
        {
            ToDoItemId = todo.Id,
            UserId = command.UserId
        });

        await _unitOfWork.SaveEntitiesAsync(cancellationToken);

        return new AddToDoCommandResult(200, string.Empty);
    }
}
