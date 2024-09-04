using MediatR;
using ToDoList.Stock.Db;
using ToDoList.Stock.Db.Domain;

namespace ToDoList.Stock.Core.Commands.Post.AddUser;

public class AddUserCommandHandler : IRequestHandler<AddUserCommand, AddUserCommandResult>
{
    private readonly IStockUnitOfWork _unitOfWork;

    public AddUserCommandHandler(IStockUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }
    public async Task<AddUserCommandResult> Handle(AddUserCommand command, CancellationToken cancellationToken)
    {
        await _unitOfWork.UserRepository.AddUser(new User()
        {
            Name = command.Name
        });

        await _unitOfWork.SaveEntitiesAsync(cancellationToken);

        return new AddUserCommandResult(200, string.Empty);
    }
}
