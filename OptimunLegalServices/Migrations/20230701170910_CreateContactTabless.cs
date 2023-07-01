using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OptimunLegalServices.Migrations
{
    public partial class CreateContactTabless : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "ContactUs",
                newName: "LastName");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "ContactUs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "ContactUs");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "ContactUs",
                newName: "Name");
        }
    }
}
