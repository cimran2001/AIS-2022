using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProfitHealAPI.Migrations
{
    public partial class Databaseupdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CategoryName",
                table: "Symptoms",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "SymptomCategories",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SymptomCategories", x => x.Name);
                });

            migrationBuilder.InsertData(
                table: "SymptomCategories",
                column: "Name",
                value: "Digestion");

            migrationBuilder.CreateIndex(
                name: "IX_Symptoms_CategoryName",
                table: "Symptoms",
                column: "CategoryName");

            migrationBuilder.CreateIndex(
                name: "IX_SymptomCategories_Name",
                table: "SymptomCategories",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Symptoms_SymptomCategories_CategoryName",
                table: "Symptoms",
                column: "CategoryName",
                principalTable: "SymptomCategories",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Symptoms_SymptomCategories_CategoryName",
                table: "Symptoms");

            migrationBuilder.DropTable(
                name: "SymptomCategories");

            migrationBuilder.DropIndex(
                name: "IX_Symptoms_CategoryName",
                table: "Symptoms");

            migrationBuilder.DropColumn(
                name: "CategoryName",
                table: "Symptoms");
        }
    }
}
