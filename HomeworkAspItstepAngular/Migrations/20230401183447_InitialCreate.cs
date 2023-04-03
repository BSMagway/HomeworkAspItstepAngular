using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeworkAspItstepAngular.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ApplicationUserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ApplicationUserId);
                });

            migrationBuilder.CreateTable(
                name: "Notepads",
                columns: table => new
                {
                    NotepadId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ApplicationUserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    NotepadName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notepads", x => x.NotepadId);
                    table.ForeignKey(
                        name: "FK_Notepads_Users_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "Users",
                        principalColumn: "ApplicationUserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notes",
                columns: table => new
                {
                    NoteId = table.Column<Guid>(type: "TEXT", nullable: false),
                    NotepadId = table.Column<Guid>(type: "TEXT", nullable: false),
                    NoteName = table.Column<string>(type: "TEXT", nullable: false),
                    NoteContent = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.NoteId);
                    table.ForeignKey(
                        name: "FK_Notes_Notepads_NotepadId",
                        column: x => x.NotepadId,
                        principalTable: "Notepads",
                        principalColumn: "NotepadId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notepads_ApplicationUserId",
                table: "Notepads",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_NotepadId",
                table: "Notes",
                column: "NotepadId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notes");

            migrationBuilder.DropTable(
                name: "Notepads");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
