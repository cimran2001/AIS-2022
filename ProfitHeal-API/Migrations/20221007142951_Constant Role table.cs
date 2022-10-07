using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProfitHealAPI.Migrations
{
    public partial class ConstantRoletable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                column: "Name",
                value: "Admin");

            migrationBuilder.InsertData(
                table: "Roles",
                column: "Name",
                value: "User");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Name",
                keyValue: "Admin");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Name",
                keyValue: "User");
        }
    }
}
