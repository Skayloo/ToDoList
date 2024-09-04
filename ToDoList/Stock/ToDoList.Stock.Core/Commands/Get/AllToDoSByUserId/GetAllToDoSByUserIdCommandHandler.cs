using MediatR;
using System.Linq.Expressions;
using ToDoList.Stock.Core.Commands.Get.ToDoByUserId;
using ToDoList.Stock.Db;
using ToDoList.Stock.Db.Domain;

namespace ToDoList.Stock.Core.Commands.Get.AllToDoSByUserId
{
    public class GetAllToDoSByUserIdCommandHandler : IRequestHandler<GetAllToDoSByUserIdCommand, GetAllToDoSByUserIdCommandResult>
    {
        private readonly IStockUnitOfWork _unitOfWork;

        public GetAllToDoSByUserIdCommandHandler(IStockUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<GetAllToDoSByUserIdCommandResult> Handle(GetAllToDoSByUserIdCommand command, CancellationToken cancellationToken)
        {
            Expression<Func<ToDoItem, bool>> filter = x => x.IsDeleted != true;

            if (command.UserId != null)
                filter = filter.And(x => x.UserId == command.UserId);

            if (command.IsCompleted != null)
                filter = filter.And(x => x.IsCompleted == command.IsCompleted);

            if (command.Priority != null)
                filter = filter.And(x => x.PriorityLevel == command.Priority);

            var userToDoS = await _unitOfWork.ToDoListRepository.GetToDoByParams(filter);

            return new GetAllToDoSByUserIdCommandResult(userToDoS);
        }
    }

    public static class ExpressionExtensions
    {
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            var parameter = Expression.Parameter(typeof(T));

            var body = Expression.AndAlso(
                Expression.Invoke(expr1, parameter),
                Expression.Invoke(expr2, parameter)
            );

            return Expression.Lambda<Func<T, bool>>(body, parameter);
        }
    }
}
