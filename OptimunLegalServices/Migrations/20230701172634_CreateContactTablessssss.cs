using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OptimunLegalServices.Migrations
{
    public partial class CreateContactTablessssss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Desc",
                table: "Blogs");

            migrationBuilder.AddColumn<string>(
                name: "Desc1",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Desc2",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Desc3",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Desc4",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Desc1",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "Desc2",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "Desc3",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "Desc4",
                table: "Blogs");

            migrationBuilder.AddColumn<string>(
                name: "Desc",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
