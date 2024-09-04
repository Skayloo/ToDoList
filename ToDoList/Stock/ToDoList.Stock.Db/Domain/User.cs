using System.Text.Json.Serialization;
using ToDoList.Domain.Abstractions;

namespace ToDoList.Stock.Db.Domain;

public class User : Entity
{
    public string Name { get; set; }

    public bool IsDeleted { get; set; }

    [JsonIgnore]
    public virtual ICollection<UsersOfToDoItems> UsersOfToDoItems { get; set; }
}
