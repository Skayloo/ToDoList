using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoList.Stock.Db.Domain;

namespace ToDoList.Stock.Db.Context.EntityConfigurations;

public class ToDoItemEntityTypeConfiguration : IEntityTypeConfiguration<ToDoItem>
{
    public void Configure(EntityTypeBuilder<ToDoItem> builder)
    {
        builder.ToTable("todo_item");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasColumnName("id").ValueGeneratedOnAdd();
        builder.Property(p => p.IsDeleted).HasColumnName("is_deleted").IsRequired();
        builder.Property(p => p.Title).HasColumnName("title").IsRequired(false);
        builder.Property(p => p.Description).HasColumnName("description").IsRequired(false);
        builder.Property(p => p.IsCompleted).HasColumnName("is_completed").IsRequired(false);
        builder.Property(p => p.DueDate).HasColumnName("due_date").IsRequired(false);
        builder.Property(p => p.UserId).HasColumnName("user_id");
        builder.Property(p => p.PriorityLevel).HasColumnName("priority_level");

        builder.HasOne(o => o.Priority)
               .WithMany(p => p.ToDoItem)
               .HasForeignKey(o => o.PriorityLevel);
    }
}