using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoList.Stock.Db.Domain;

namespace ToDoList.Stock.Db.Context.EntityConfigurations;

public class PriorityEntityTypeConfiguration : IEntityTypeConfiguration<Priority>
{
    public void Configure(EntityTypeBuilder<Priority> builder)
    {
        builder.ToTable("priority");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasColumnName("id").ValueGeneratedOnAdd();
        builder.Property(p => p.Level).HasColumnName("level").IsRequired();
        builder.Property(p => p.Name).HasColumnName("name").HasMaxLength(300).IsRequired();

        builder.HasMany(p => p.ToDoItem)
                .WithOne(o => o.Priority)
                .HasForeignKey(o => o.PriorityLevel);
    }
}
