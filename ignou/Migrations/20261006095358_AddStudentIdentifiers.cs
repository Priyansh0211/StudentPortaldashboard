using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ignou.Migrations
{
    /// <inheritdoc />
    public partial class AddStudentIdentifiers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationNumber",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EnrollmentNumber",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApplicationNumber",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "EnrollmentNumber",
                table: "Users");
        }
    }
}
