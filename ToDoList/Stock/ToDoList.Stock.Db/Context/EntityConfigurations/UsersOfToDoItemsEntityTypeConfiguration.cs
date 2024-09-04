using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoList.Stock.Db.Domain;

namespace ToDoList.Stock.Db.Context.EntityConfigurations;

public class UsersOfToDoItemsEntityTypeConfiguration : IEntityTypeConfiguration<UsersOfToDoItems>
{
    public void Configure(EntityTypeBuilder<UsersOfToDoItems> builder)
    {
        builder.ToTable("users_of_todo_items");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasColumnName("id").ValueGeneratedOnAdd();
        builder.Property(p => p.UserId).HasColumnName("user_id");
        builder.Property(p => p.ToDoItemId).HasColumnName("todo_item_id");
        builder.Property(p => p.IsDeleted).HasColumnName("is_deleted");

        builder.HasOne(ep => ep.User)
                .WithMany(e => e.UsersOfToDoItems)
                .HasForeignKey(ep => ep.UserId);

        builder.HasOne(m => m.ToDoItem)
            .WithMany(e => e.UsersOfToDoItems)
            .HasForeignKey(m => m.ToDoItemId);
    }
}
