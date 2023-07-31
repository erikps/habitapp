using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace habitapp.Migrations
{
    /// <inheritdoc />
    public partial class AddHabitCompletions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HabitCompletions",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CompletedHabitid = table.Column<int>(type: "INTEGER", nullable: false),
                    CompletedOn = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HabitCompletions", x => x.id);
                    table.ForeignKey(
                        name: "FK_HabitCompletions_Habit_CompletedHabitid",
                        column: x => x.CompletedHabitid,
                        principalTable: "Habit",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HabitCompletions_CompletedHabitid",
                table: "HabitCompletions",
                column: "CompletedHabitid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HabitCompletions");
        }
    }
}
