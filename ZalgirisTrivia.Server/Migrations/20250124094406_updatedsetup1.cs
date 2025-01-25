using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZalgirisTrivia.Server.Migrations
{
    /// <inheritdoc />
    public partial class updatedsetup1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Answer",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0)
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<string>(
                name: "UserAnswer",
                table: "Answer",
                type: "TEXT",
                nullable: false,
                defaultValue: "[]");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Answer",
                table: "Answer",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Answer_SubmissionId",
                table: "Answer",
                column: "SubmissionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Answer",
                table: "Answer");

            migrationBuilder.DropIndex(
                name: "IX_Answer_SubmissionId",
                table: "Answer");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Answer");

            migrationBuilder.DropColumn(
                name: "UserAnswer",
                table: "Answer");
        }
    }
}
