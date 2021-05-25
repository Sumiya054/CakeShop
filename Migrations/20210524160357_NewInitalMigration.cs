using Microsoft.EntityFrameworkCore.Migrations;

namespace CakeShop.Migrations
{
    public partial class NewInitalMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsPieOfTheWeek",
                table: "Cakes",
                newName: "IsCakesOfTheWeek");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsCakesOfTheWeek",
                table: "Cakes",
                newName: "IsPieOfTheWeek");
        }
    }
}
