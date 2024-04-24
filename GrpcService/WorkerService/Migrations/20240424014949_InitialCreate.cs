using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WorkerService.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Student");

            migrationBuilder.CreateTable(
                name: "Students",
                schema: "Student",
                columns: table => new
                {
                    StudentId = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Age = table.Column<int>(type: "integer", nullable: false),
                    Course = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.StudentId);
                });

            migrationBuilder.InsertData(
                schema: "Student",
                table: "Students",
                columns: new[] { "StudentId", "Age", "Course", "Name" },
                values: new object[,]
                {
                    { "1", 32, "C#", "이건우" },
                    { "2", 32, "JAVA", "안성윤" },
                    { "3", 32, "JAVASCRIPT", "장동계" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Students",
                schema: "Student");
        }
    }
}
