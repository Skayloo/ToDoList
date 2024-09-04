using FluentValidation;

namespace ToDoList.Stock.Core.Commands.Delete.DeleteUser;

public class DeleteUserValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserValidator()
    {
        RuleFor(c => c.UserId).GreaterThan(0).NotEmpty().WithMessage("Todo item id required");
    }
}
