using Microsoft.EntityFrameworkCore.Migrations;

namespace ExpReadersBack.Migrations
{
    public partial class UpdateUserStats : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LastReadBook",
                table: "UserStatistics",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastReadBook",
                table: "UserStatistics");
        }
    }
}
