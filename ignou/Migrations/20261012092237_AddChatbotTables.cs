using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ignou.Migrations
{
    /// <inheritdoc />
    public partial class AddChatbotTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChatbotCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatbotCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChatbotFAQs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Question = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(3000)", maxLength: 3000, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatbotFAQs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatbotFAQs_ChatbotCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "ChatbotCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChatbotOptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FaqId = table.Column<int>(type: "int", nullable: false),
                    OptionText = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    NextFaqId = table.Column<int>(type: "int", nullable: true),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatbotOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatbotOptions_ChatbotFAQs_FaqId",
                        column: x => x.FaqId,
                        principalTable: "ChatbotFAQs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChatbotOptions_ChatbotFAQs_NextFaqId",
                        column: x => x.NextFaqId,
                        principalTable: "ChatbotFAQs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatbotCategories_Name",
                table: "ChatbotCategories",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChatbotFAQs_CategoryId",
                table: "ChatbotFAQs",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatbotOptions_FaqId",
                table: "ChatbotOptions",
                column: "FaqId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatbotOptions_NextFaqId",
                table: "ChatbotOptions",
                column: "NextFaqId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatbotOptions");

            migrationBuilder.DropTable(
                name: "ChatbotFAQs");

            migrationBuilder.DropTable(
                name: "ChatbotCategories");
        }
    }
}
