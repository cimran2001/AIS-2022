using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProfitHealAPI.Migrations
{
    public partial class Symptomcategoriesadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "SymptomCategories",
                column: "Name",
                values: new object[]
                {
                    "Dermis",
                    "Nerves",
                    "Other",
                    "Pain",
                    "Respiration",
                    "Senses"
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SymptomCategories",
                keyColumn: "Name",
                keyValue: "Dermis");

            migrationBuilder.DeleteData(
                table: "SymptomCategories",
                keyColumn: "Name",
                keyValue: "Nerves");

            migrationBuilder.DeleteData(
                table: "SymptomCategories",
                keyColumn: "Name",
                keyValue: "Other");

            migrationBuilder.DeleteData(
                table: "SymptomCategories",
                keyColumn: "Name",
                keyValue: "Pain");

            migrationBuilder.DeleteData(
                table: "SymptomCategories",
                keyColumn: "Name",
                keyValue: "Respiration");

            migrationBuilder.DeleteData(
                table: "SymptomCategories",
                keyColumn: "Name",
                keyValue: "Senses");
        }
    }
}
