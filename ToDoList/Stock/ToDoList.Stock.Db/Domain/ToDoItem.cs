using Newtonsoft.Json;
using ToDoList.Domain.Abstractions;

namespace ToDoList.Stock.Db.Domain;

public class ToDoItem : Entity
{
    public bool IsDeleted { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public bool? IsCompleted { get; set; }

    public DateTime? DueDate { get; set; }

    public int UserId { get; set; }

    public int PriorityLevel { get; set; }

    public virtual Priority Priority { get; set; }

    [JsonIgnore]
    public virtual ICollection<UsersOfToDoItems> UsersOfToDoItems { get; set; }
}
