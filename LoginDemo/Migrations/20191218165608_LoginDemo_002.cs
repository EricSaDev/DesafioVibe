using Microsoft.EntityFrameworkCore.Migrations;

namespace LoginDemo.Migrations
{
    public partial class LoginDemo_002 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LOCALACCESSTOKEN",
                table: "User",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LOCALACCESSTOKEN",
                table: "User");
        }
    }
}
