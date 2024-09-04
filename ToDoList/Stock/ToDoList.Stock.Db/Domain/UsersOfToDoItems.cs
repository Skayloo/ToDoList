using ToDoList.Domain.Abstractions;

namespace ToDoList.Stock.Db.Domain;

public class UsersOfToDoItems : Entity
{
    public bool IsDeleted { get; set; }

    public int UserId { get; set; }

    public int ToDoItemId { get; set; }

    public virtual User User { get; set; }

    public virtual ToDoItem ToDoItem { get; set; }
}
