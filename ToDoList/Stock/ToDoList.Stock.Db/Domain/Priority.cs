using ToDoList.Domain.Abstractions;

namespace ToDoList.Stock.Db.Domain;

public class Priority : Entity
{
    public int? Level { get; set; }

    public string Name { get; set; }

    public virtual ICollection<ToDoItem> ToDoItem { get; set; }
}
