using FluentValidation;

namespace ToDoList.Stock.Core.Commands.Patch.UpdateUser
{
    internal class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserValidator()
        {
            RuleFor(c => c.UserId)
                .NotEmpty()
                .GreaterThan(0)
                .WithMessage("User Id must be a positive number!");

            RuleFor(c => c.Name).NotEmpty().WithMessage("Username required!");
        }
    }
}
