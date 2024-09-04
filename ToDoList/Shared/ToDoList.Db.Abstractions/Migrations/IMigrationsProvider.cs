namespace ToDoList.Db.Abstractions.Migrations;

public interface IMigrationsProvider
{
    int Index { get; }

    bool Migrate();
}
