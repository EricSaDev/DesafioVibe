using Microsoft.EntityFrameworkCore.Migrations;

namespace LoginDemo.Migrations
{
    public partial class LoginDemo_003 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ULTIMOLOGIN",
                table: "User",
                newName: "VALIDADELOGIN");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VALIDADELOGIN",
                table: "User",
                newName: "ULTIMOLOGIN");
        }
    }
}
