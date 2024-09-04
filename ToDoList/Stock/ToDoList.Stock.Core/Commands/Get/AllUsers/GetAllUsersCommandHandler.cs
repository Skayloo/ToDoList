using MediatR;
using ToDoList.Stock.Db;

namespace ToDoList.Stock.Core.Commands.Get.AllUsers;

public class GetAllUsersCommandHandler : IRequestHandler<GetAllUsersCommand, GetAllUsersCommandResult>
{
    private readonly IStockUnitOfWork _unitOfWork;

    public GetAllUsersCommandHandler(IStockUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }
    public async Task<GetAllUsersCommandResult> Handle(GetAllUsersCommand command, CancellationToken cancellationToken)
    {
        return new GetAllUsersCommandResult(await _unitOfWork.UserRepository.GetAllUsers());
    }
}
