using Microsoft.EntityFrameworkCore.Migrations;

namespace ConsoleTests.Migrations
{
    public partial class RemoveDescrptionFromStudent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Students");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
