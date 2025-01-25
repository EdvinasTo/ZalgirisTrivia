using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZalgirisTrivia.Server.Migrations
{
    /// <inheritdoc />
    public partial class updatedsetup2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AnswersJson",
                table: "Submissions",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnswersJson",
                table: "Submissions");
        }
    }
}
