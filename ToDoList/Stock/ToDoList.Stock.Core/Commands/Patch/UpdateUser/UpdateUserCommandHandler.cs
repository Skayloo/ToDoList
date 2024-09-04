using MediatR;
using ToDoList.Stock.Db;

namespace ToDoList.Stock.Core.Commands.Patch.UpdateUser;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UpdateUserCommandResult>
{
    private readonly IStockUnitOfWork _unitOfWork;

    public UpdateUserCommandHandler(IStockUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<UpdateUserCommandResult> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepository.GetUserByParams(x => x.Id == command.UserId && x.IsDeleted != true);

        if (user == null)
            return new UpdateUserCommandResult(404, "User not found");

        user.Name = command.Name;

        await _unitOfWork.UserRepository.UpdateUser(user);

        await _unitOfWork.SaveEntitiesAsync(cancellationToken);

        return new UpdateUserCommandResult(200, string.Empty);
    }
}
