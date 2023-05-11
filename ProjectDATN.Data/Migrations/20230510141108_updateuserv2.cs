using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectDATN.Data.Migrations
{
    public partial class updateuserv2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Huyen",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tinh",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Xa",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Huyen",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Tinh",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Xa",
                table: "Users");
        }
    }
}
