using FluentValidation;
using ToDoList.Stock.Core.Commands.Post.AddToDo;

namespace ToDoList.Stock.Core.Commands.Patch.UpdateToDo
{
    public class UpdateToDoValidator : AbstractValidator<AddToDoCommand>
    {
        public UpdateToDoValidator()
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
}
