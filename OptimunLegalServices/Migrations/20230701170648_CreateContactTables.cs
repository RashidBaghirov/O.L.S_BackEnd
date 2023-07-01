using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OptimunLegalServices.Migrations
{
    public partial class CreateContactTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PracticeAreaId",
                table: "ContactUs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ContactUs_PracticeAreaId",
                table: "ContactUs",
                column: "PracticeAreaId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContactUs_PracticeAreas_PracticeAreaId",
                table: "ContactUs",
                column: "PracticeAreaId",
                principalTable: "PracticeAreas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactUs_PracticeAreas_PracticeAreaId",
                table: "ContactUs");

            migrationBuilder.DropIndex(
                name: "IX_ContactUs_PracticeAreaId",
                table: "ContactUs");

            migrationBuilder.DropColumn(
                name: "PracticeAreaId",
                table: "ContactUs");
        }
    }
}
