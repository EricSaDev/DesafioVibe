using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LoginDemo.Migrations
{
    public partial class LoginDemo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    CPF = table.Column<string>(nullable: true),
                    NOME = table.Column<string>(nullable: true),
                    ESPECIAL = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    CPF = table.Column<string>(nullable: false),
                    NOME = table.Column<string>(nullable: true),
                    NASCIMENTO = table.Column<DateTime>(nullable: false),
                    SENHA = table.Column<string>(nullable: true),
                    PERFIL = table.Column<string>(nullable: true),
                    APIACCESSTOKEN = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.CPF);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cliente");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
