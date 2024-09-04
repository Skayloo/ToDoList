using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ToDoList.Stock.Db.Migrations
{
    /// <inheritdoc />
    public partial class AllInDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "priority",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    level = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_priority", x => x.id);
                });

            migrationBuilder.InsertData(table: "priority", columns: new[] { "level", "name" },
                values: new object[] { "1", "Срочно" });
            migrationBuilder.InsertData(table: "priority", columns: new[] { "level", "name" },
                values: new object[] { "2", "Очень срочно" });
            migrationBuilder.InsertData(table: "priority", columns: new[] { "level", "name" },
                values: new object[] { "3", "Ну прям крайне срочно" });
            migrationBuilder.InsertData(table: "priority", columns: new[] { "level", "name" },
                values: new object[] { "4", "Турбо срочно" });
            migrationBuilder.InsertData(table: "priority", columns: new[] { "level", "name" },
                values: new object[] { "5", "Ядерно срочно" });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "todo_item",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    title = table.Column<string>(type: "text", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    is_completed = table.Column<bool>(type: "boolean", nullable: true),
                    due_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    priority_level = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_todo_item", x => x.id);
                    table.ForeignKey(
                        name: "FK_todo_item_priority_priority_level",
                        column: x => x.priority_level,
                        principalTable: "priority",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "users_of_todo_items",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    todo_item_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users_of_todo_items", x => x.id);
                    table.ForeignKey(
                        name: "FK_users_of_todo_items_todo_item_todo_item_id",
                        column: x => x.todo_item_id,
                        principalTable: "todo_item",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_users_of_todo_items_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_todo_item_priority_level",
                table: "todo_item",
                column: "priority_level");

            migrationBuilder.CreateIndex(
                name: "IX_users_of_todo_items_todo_item_id",
                table: "users_of_todo_items",
                column: "todo_item_id");

            migrationBuilder.CreateIndex(
                name: "IX_users_of_todo_items_user_id",
                table: "users_of_todo_items",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "users_of_todo_items");

            migrationBuilder.DropTable(
                name: "todo_item");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "priority");
        }
    }
}
