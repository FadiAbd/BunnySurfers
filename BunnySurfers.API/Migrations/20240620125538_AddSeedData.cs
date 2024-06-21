using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BunnySurfers.API.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "CourseUser",
                keyColumns: new[] { "CoursesCourseId", "UsersUserId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "CourseUser",
                keyColumns: new[] { "CoursesCourseId", "UsersUserId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "CourseUser",
                keyColumns: new[] { "CoursesCourseId", "UsersUserId" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "CourseUser",
                keyColumns: new[] { "CoursesCourseId", "UsersUserId" },
                keyValues: new object[] { 1, 4 });

            migrationBuilder.DeleteData(
                table: "CourseUser",
                keyColumns: new[] { "CoursesCourseId", "UsersUserId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "Documents",
                keyColumn: "DocumentId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Documents",
                keyColumn: "DocumentId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Documents",
                keyColumn: "DocumentId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Modules",
                keyColumn: "ModuleId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Modules",
                keyColumn: "ModuleId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Modules",
                keyColumn: "ModuleId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Modules",
                keyColumn: "ModuleId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 1);
        }
    }
}
