using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUserPermission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserPermissionPermissionId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserPermissionUserId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserPermissions",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PermissionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPermissions", x => new { x.UserId, x.PermissionId });
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserPermissionUserId_UserPermissionPermissionId",
                table: "AspNetUsers",
                columns: new[] { "UserPermissionUserId", "UserPermissionPermissionId" });

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_UserPermissions_UserPermissionUserId_UserPermissionPermissionId",
                table: "AspNetUsers",
                columns: new[] { "UserPermissionUserId", "UserPermissionPermissionId" },
                principalTable: "UserPermissions",
                principalColumns: new[] { "UserId", "PermissionId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_UserPermissions_UserPermissionUserId_UserPermissionPermissionId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "UserPermissions");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_UserPermissionUserId_UserPermissionPermissionId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserPermissionPermissionId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserPermissionUserId",
                table: "AspNetUsers");
        }
    }
}
