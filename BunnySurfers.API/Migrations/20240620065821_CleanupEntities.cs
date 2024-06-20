using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BunnySurfers.API.Migrations
{
    /// <inheritdoc />
    public partial class CleanupEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documents_Users_UploadedByUserId",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Activities");

            migrationBuilder.AddColumn<DateTime>(
                name: "StartTime",
                table: "Activities",
                type: "datetime2",
                nullable: true);

            migrationBuilder.RenameColumn(
                name: "UploadedByUserId",
                table: "Documents",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Documents_UploadedByUserId",
                table: "Documents",
                newName: "IX_Documents_UserId");

            migrationBuilder.RenameColumn(
                name: "EndDate",
                table: "Activities",
                newName: "EndTime");

            migrationBuilder.AddColumn<string>(
                name: "LocalFilePath",
                table: "Documents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UntrustedFileName",
                table: "Documents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_Users_UserId",
                table: "Documents",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documents_Users_UserId",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "LocalFilePath",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "UntrustedFileName",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "Activities");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Documents",
                newName: "UploadedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Documents_UserId",
                table: "Documents",
                newName: "IX_Documents_UploadedByUserId");

            migrationBuilder.RenameColumn(
                name: "StartTime",
                table: "Activities",
                newName: "EndDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Activities",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_Users_UploadedByUserId",
                table: "Documents",
                column: "UploadedByUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
