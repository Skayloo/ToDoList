using FluentValidation;

namespace ToDoList.Stock.Core.Commands.Post.AddToDo;

public class AddToDoValidator : AbstractValidator<AddToDoCommand>
{
    public AddToDoValidator()
    {
        RuleFor(c => c.UserId)
            .NotEmpty()
            .GreaterThan(0)
            .WithMessage("User Id must be a positive number!");

        RuleFor(c => c.IsCompleted)
            .NotEmpty()
            .WithMessage("Choose the completed stage!");

        RuleFor(c => c.PriorityLevel)
            .NotEmpty()
            .GreaterThan(0)
            .WithMessage("Priority level required!");
    }
}
