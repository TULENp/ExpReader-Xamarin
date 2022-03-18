using Microsoft.EntityFrameworkCore.Migrations;

namespace ExpReadersBack.Migrations
{
    public partial class UpdateBook : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrentPage",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentPage",
                table: "Books");
        }
    }
}
