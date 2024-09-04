using FluentValidation;

namespace ToDoList.Stock.Core.Commands.Delete.DeleteToDo;

public class DeleteToDoValidator : AbstractValidator<DeleteToDoCommand>
{
    public DeleteToDoValidator()
    {
        RuleFor(c => c.Id).NotEmpty().WithMessage("Todo item id required");
    }
}
