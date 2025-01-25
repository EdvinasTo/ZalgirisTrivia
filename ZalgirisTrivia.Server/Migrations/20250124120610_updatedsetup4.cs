using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZalgirisTrivia.Server.Migrations
{
    /// <inheritdoc />
    public partial class updatedsetup4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AnswersJson",
                table: "Submissions",
                type: "TEXT",
                nullable: true,
                defaultValue: "[]",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AnswersJson",
                table: "Submissions",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true,
                oldDefaultValue: "[]");
        }
    }
}
