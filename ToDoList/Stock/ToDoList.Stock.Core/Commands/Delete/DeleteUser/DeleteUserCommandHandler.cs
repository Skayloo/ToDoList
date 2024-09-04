using MediatR;
using ToDoList.Stock.Db;

namespace ToDoList.Stock.Core.Commands.Delete.DeleteUser;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, DeleteUserCommandResult>
{
    private readonly IStockUnitOfWork _unitOfWork;

    public DeleteUserCommandHandler(IStockUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<DeleteUserCommandResult> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepository.GetUserByParams(x => x.Id == command.UserId && x.IsDeleted != true);

        if (user == null)
            return new DeleteUserCommandResult(404, "User not found");

        user.IsDeleted = true;

        await _unitOfWork.UserRepository.UpdateUser(user);

        await _unitOfWork.SaveEntitiesAsync(cancellationToken);

        return new DeleteUserCommandResult(200, string.Empty);
    }
}
