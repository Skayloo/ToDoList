namespace ToDoList.Domain.Abstractions;

public interface IDomainEvent
{
    DateTime OccurredOn { get; }
}
