using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZalgirisTrivia.Server.Migrations
{
    /// <inheritdoc />
    public partial class updatedsetup8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnswersJson",
                table: "Submissions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AnswersJson",
                table: "Submissions",
                type: "TEXT",
                nullable: true,
                defaultValue: "[]");
        }
    }
}
