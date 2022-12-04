using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProfitHealAPI.Migrations
{
    public partial class Reportsclassupdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Symptoms_SymptomName",
                table: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_Reports_SymptomName",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "SymptomName",
                table: "Reports");

            migrationBuilder.AddColumn<int>(
                name: "ReportId",
                table: "Symptoms",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Symptoms_ReportId",
                table: "Symptoms",
                column: "ReportId");

            migrationBuilder.AddForeignKey(
                name: "FK_Symptoms_Reports_ReportId",
                table: "Symptoms",
                column: "ReportId",
                principalTable: "Reports",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Symptoms_Reports_ReportId",
                table: "Symptoms");

            migrationBuilder.DropIndex(
                name: "IX_Symptoms_ReportId",
                table: "Symptoms");

            migrationBuilder.DropColumn(
                name: "ReportId",
                table: "Symptoms");

            migrationBuilder.AddColumn<string>(
                name: "SymptomName",
                table: "Reports",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_SymptomName",
                table: "Reports",
                column: "SymptomName");

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Symptoms_SymptomName",
                table: "Reports",
                column: "SymptomName",
                principalTable: "Symptoms",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
