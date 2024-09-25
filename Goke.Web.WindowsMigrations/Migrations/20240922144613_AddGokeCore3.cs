using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Goke.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddGokeCore3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_UserDetails_UserDetailId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserDetailId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_UserDetails_UserDetailId",
                table: "AspNetUsers",
                column: "UserDetailId",
                principalTable: "UserDetails",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_UserDetails_UserDetailId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserDetailId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_UserDetails_UserDetailId",
                table: "AspNetUsers",
                column: "UserDetailId",
                principalTable: "UserDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
