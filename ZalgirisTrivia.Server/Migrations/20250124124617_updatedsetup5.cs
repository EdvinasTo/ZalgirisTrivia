using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZalgirisTrivia.Server.Migrations
{
    /// <inheritdoc />
    public partial class updatedsetup5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answer_Submissions_SubmissionId",
                table: "Answer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Submissions",
                table: "Submissions");

            migrationBuilder.RenameTable(
                name: "Submissions",
                newName: "Submissions1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Submissions1",
                table: "Submissions1",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Answer_Submissions1_SubmissionId",
                table: "Answer",
                column: "SubmissionId",
                principalTable: "Submissions1",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answer_Submissions1_SubmissionId",
                table: "Answer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Submissions1",
                table: "Submissions1");

            migrationBuilder.RenameTable(
                name: "Submissions1",
                newName: "Submissions");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Submissions",
                table: "Submissions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Answer_Submissions_SubmissionId",
                table: "Answer",
                column: "SubmissionId",
                principalTable: "Submissions",
                principalColumn: "Id");
        }
    }
}
