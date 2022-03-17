using Microsoft.EntityFrameworkCore.Migrations;

namespace ExpReadersBack.Migrations
{
    public partial class ChangeUserBook : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrentPage",
                table: "UserBooks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsRead",
                table: "UserBooks",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentPage",
                table: "UserBooks");

            migrationBuilder.DropColumn(
                name: "IsRead",
                table: "UserBooks");
        }
    }
}
