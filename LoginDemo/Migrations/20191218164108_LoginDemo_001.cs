using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LoginDemo.Migrations
{
    public partial class LoginDemo_001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ULTIMOLOGIN",
                table: "User",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "USERCPF",
                table: "Cliente",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_USERCPF",
                table: "Cliente",
                column: "USERCPF");

            migrationBuilder.AddForeignKey(
                name: "FK_Cliente_User_USERCPF",
                table: "Cliente",
                column: "USERCPF",
                principalTable: "User",
                principalColumn: "CPF",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cliente_User_USERCPF",
                table: "Cliente");

            migrationBuilder.DropIndex(
                name: "IX_Cliente_USERCPF",
                table: "Cliente");

            migrationBuilder.DropColumn(
                name: "ULTIMOLOGIN",
                table: "User");

            migrationBuilder.DropColumn(
                name: "USERCPF",
                table: "Cliente");
        }
    }
}
