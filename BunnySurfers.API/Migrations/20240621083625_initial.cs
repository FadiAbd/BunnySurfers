using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BunnySurfers.API.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    CourseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.CourseId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Role = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Modules",
                columns: table => new
                {
                    ModuleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modules", x => x.ModuleId);
                    table.ForeignKey(
                        name: "FK_Modules_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseUser",
                columns: table => new
                {
                    CoursesCourseId = table.Column<int>(type: "int", nullable: false),
                    UsersUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseUser", x => new { x.CoursesCourseId, x.UsersUserId });
                    table.ForeignKey(
                        name: "FK_CourseUser_Courses_CoursesCourseId",
                        column: x => x.CoursesCourseId,
                        principalTable: "Courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseUser_Users_UsersUserId",
                        column: x => x.UsersUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Activities",
                columns: table => new
                {
                    ActivityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActivityType = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModuleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activities", x => x.ActivityId);
                    table.ForeignKey(
                        name: "FK_Activities_Modules_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "Modules",
                        principalColumn: "ModuleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    DocumentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeOfUpload = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UntrustedFileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocalFilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: true),
                    ModuleId = table.Column<int>(type: "int", nullable: true),
                    ActivityId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.DocumentId);
                    table.ForeignKey(
                        name: "FK_Documents_Activities_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "Activities",
                        principalColumn: "ActivityId");
                    table.ForeignKey(
                        name: "FK_Documents_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "CourseId");
                    table.ForeignKey(
                        name: "FK_Documents_Modules_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "Modules",
                        principalColumn: "ModuleId");
                    table.ForeignKey(
                        name: "FK_Documents_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "CourseId", "Description", "EndDate", "Name", "StartDate" },
                values: new object[,]
                {
                    { 1, "Teknik och verktyg att jobba med både relational databas (SQL) och non-relational databas (NoSQL)", new DateOnly(2024, 10, 15), "Databasteknik", new DateOnly(2024, 9, 1) },
                    { 2, "Koppla kunskap från Databasteknik till en Object-Relational Mapper med Entity Framework Core", new DateOnly(2024, 12, 15), "Entity Framework Core", new DateOnly(2024, 11, 1) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "Name", "Role" },
                values: new object[,]
                {
                    { 1, "lara.ren@lexicon.se", "Lara Ren", 2 },
                    { 2, "elle.eve@hotmail.com", "Elle Eve", 1 },
                    { 3, "stuart.dent@hotmail.com", "Stuart Dent", 1 },
                    { 4, "karlkarl@gmail.com", "Karl Karlsson", 1 },
                    { 5, "hackerman@tpu.org", "Uno Thorized", 0 }
                });

            migrationBuilder.InsertData(
                table: "CourseUser",
                columns: new[] { "CoursesCourseId", "UsersUserId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 1, 3 },
                    { 1, 4 },
                    { 2, 1 }
                });

            migrationBuilder.InsertData(
                table: "Documents",
                columns: new[] { "DocumentId", "ActivityId", "CourseId", "Description", "LocalFilePath", "ModuleId", "Name", "TimeOfUpload", "UntrustedFileName", "UserId" },
                values: new object[] { 1, null, 1, "Schedule", "kursschema.pdf", null, "Kursschema", new DateTime(2024, 8, 1, 9, 0, 0, 0, DateTimeKind.Unspecified), "kursschema.pdf", 1 });

            migrationBuilder.InsertData(
                table: "Modules",
                columns: new[] { "ModuleId", "CourseId", "Description", "EndDate", "Name", "StartDate" },
                values: new object[,]
                {
                    { 1, 1, "Relational DataBase Management Systems (RDBMS) som SQL; ACID; normalized tables; queries and performance", new DateOnly(2024, 9, 15), "Relational databas", new DateOnly(2024, 9, 1) },
                    { 2, 1, "NoSQL systems; document data stores; graph databases; design trade-offs with ACID", new DateOnly(2024, 9, 30), "Non-relational databas", new DateOnly(2024, 9, 16) },
                    { 3, 1, "Distributed and cloud architecture; transactions and consistency", new DateOnly(2024, 10, 15), "Distributed databas", new DateOnly(2024, 10, 1) },
                    { 4, 2, "", new DateOnly(2024, 11, 1), "Introduction", new DateOnly(2024, 11, 1) }
                });

            migrationBuilder.InsertData(
                table: "Activities",
                columns: new[] { "ActivityId", "ActivityType", "Description", "EndTime", "ModuleId", "Name", "StartTime" },
                values: new object[,]
                {
                    { 1, 1, "Introduction to the course", new DateTime(2024, 9, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), 1, "Introduction", new DateTime(2024, 9, 1, 10, 30, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 4, "Code-along demo session on creating tables in MySQL", new DateTime(2024, 9, 4, 16, 0, 0, 0, DateTimeKind.Unspecified), 1, "Hands-on with MySQL", new DateTime(2024, 9, 4, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 2, "Review exercises on SQL query syntax (select, where, join)", new DateTime(2024, 9, 10, 17, 0, 0, 0, DateTimeKind.Unspecified), 1, "Query syntax review", new DateTime(2024, 9, 7, 9, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 3, "Basic differences between relational and non-relational databases", new DateTime(2024, 9, 16, 10, 0, 0, 0, DateTimeKind.Unspecified), 2, "NoSQL basics", new DateTime(2024, 9, 16, 10, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, 1, "Introduction to the course", new DateTime(2024, 11, 1, 10, 30, 0, 0, DateTimeKind.Unspecified), 4, "Introduction", new DateTime(2024, 11, 1, 9, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Documents",
                columns: new[] { "DocumentId", "ActivityId", "CourseId", "Description", "LocalFilePath", "ModuleId", "Name", "TimeOfUpload", "UntrustedFileName", "UserId" },
                values: new object[,]
                {
                    { 2, null, null, "Instructions to set up MySQL", "set_up_mysql.docx", 1, "Getting started", new DateTime(2024, 8, 2, 10, 0, 0, 0, DateTimeKind.Unspecified), "set up mysql.docx", 1 },
                    { 3, 3, null, "Submission of assignment 1", "hw1_elle_eve.txt", null, "Assignment 1", new DateTime(2024, 9, 10, 13, 0, 0, 0, DateTimeKind.Unspecified), "hw1.sh", 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Activities_ModuleId",
                table: "Activities",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseUser_UsersUserId",
                table: "CourseUser",
                column: "UsersUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_ActivityId",
                table: "Documents",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_CourseId",
                table: "Documents",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_ModuleId",
                table: "Documents",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_UserId",
                table: "Documents",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Modules_CourseId",
                table: "Modules",
                column: "CourseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseUser");

            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropTable(
                name: "Activities");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Modules");

            migrationBuilder.DropTable(
                name: "Courses");
        }
    }
}
