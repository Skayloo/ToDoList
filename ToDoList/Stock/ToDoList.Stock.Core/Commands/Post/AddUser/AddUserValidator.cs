using FluentValidation;

namespace ToDoList.Stock.Core.Commands.Post.AddUser;

public class AddUserValidator : AbstractValidator<AddUserCommand>
{
    public AddUserValidator()
    {
        RuleFor(c => c.Name).NotEmpty().WithMessage("Username required!");
    }
}
