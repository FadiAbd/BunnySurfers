using BunnySurfers.API.Entities;

namespace BunnySurfers.API.Data
{
    public static class SeedData
    {
        // Delete and re-initialize the database (without seed data)
        public async static Task ClearDatabaseAsync(LMSDbContext context)
        {
            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();
        }

        // Clear the database and add seed data
        public async static Task ResetDatabaseAsync(LMSDbContext context)
        {
            await ClearDatabaseAsync(context);
            await AddSeedDataAsync(context);
        }

        // Add the seed data
        public async static Task AddSeedDataAsync(LMSDbContext context)
        {
            User[] users = [
                new User { Role = UserRole.Teacher, Name = "Lara Ren"     , Email = "lara.ren@lexicon.se"     },
                new User { Role = UserRole.Student, Name = "Elle Eve"     , Email = "elle.eve@hotmail.com"    },
                new User { Role = UserRole.Student, Name = "Stuart Dent"  , Email = "stuart.dent@hotmail.com" },
                new User { Role = UserRole.Student, Name = "Karl Karlsson", Email = "karlkarl@gmail.com"      },
                new User { Role = UserRole.None   , Name = "Uno Thorized" , Email = "hackerman@tpu.org"       }
            ];
            await context.AddRangeAsync(users);

            Document[] documents = [
                new Document {
                    Name = "Kursschema", Description = "Schedule", UntrustedFileName = "kursschema.pdf", LocalFilePath = "kursschema.pdf",
                    TimeOfUpload = new DateTime(2024, 08, 01, 09, 00, 00),
                    UploadedBy = users[0] },
                new Document {
                    Name = "Getting started", Description = "Instructions to set up MySQL", UntrustedFileName = "set up mysql.docx", LocalFilePath = "set_up_mysql.docx",
                    TimeOfUpload = new DateTime(2024, 08, 02, 10, 00, 00),
                    UploadedBy = users[0] },
                new Document {
                    Name = "Assignment 1", Description = "Submission of assignment 1", UntrustedFileName = "hw1.sh", LocalFilePath = "hw1_elle_eve.txt",
                    TimeOfUpload = new DateTime(2024, 09, 10, 13, 00, 00),
                    UploadedBy = users[1] },
            ];

            Course[] courses = [
                new Course {
                    Name = "Databasteknik", StartDate = new DateOnly(2024, 09, 01), EndDate = new DateOnly(2024, 10, 15),
                    Description = "Teknik och verktyg att jobba med både relational databas (SQL) och non-relational databas (NoSQL)",
                    Users = [.. users[..^1]], Documents = [documents[0]] },
                new Course {
                    Name = "Entity Framework Core", StartDate = new DateOnly(2024, 11, 01), EndDate = new DateOnly(2024, 12, 15),
                    Description = "Koppla kunskap från Databasteknik till en Object-Relational Mapper med Entity Framework Core",
                    Users = [users[0]] },
            ];
            await context.AddRangeAsync(courses);

            Module[] modules = [
                new Module {
                    Course = courses[0], Name = "Relational databas",
                    StartDate = new DateOnly(2024, 09, 01), EndDate = new DateOnly(2024, 09, 15),
                    Description = "Relational DataBase Management Systems (RDBMS) som SQL; ACID; normalized tables; queries and performance",
                    Documents = [documents[1]] },
                new Module {
                    Course = courses[0], Name = "Non-relational databas",
                    StartDate = new DateOnly(2024, 09, 16), EndDate = new DateOnly(2024, 09, 30),
                    Description = "NoSQL systems; document data stores; graph databases; design trade-offs with ACID" },
                new Module {
                    Course = courses[0], Name = "Distributed databas",
                    StartDate = new DateOnly(2024, 10, 01), EndDate = new DateOnly(2024, 10, 15),
                    Description = "Distributed and cloud architecture; transactions and consistency" },
                new Module {
                    Course = courses[1], Name = "Introduction",
                    StartDate = new DateOnly(2024, 11, 01), EndDate = new DateOnly(2024, 11, 01),
                    Description = "" }
            ];
            await context.AddRangeAsync(modules);

            Activity[] activities = [
                new Activity {
                    Module = modules[0], ActivityType = ActivityType.Lecture, Name = "Introduction",
                    StartTime = new DateTime(2024, 09, 01, 10, 30, 00), EndTime = new DateTime(2024, 09, 01, 12, 00, 00),
                    Description = "Introduction to the course" },
                new Activity {
                    Module = modules[0], ActivityType = ActivityType.Practice, Name = "Hands-on with MySQL",
                    StartTime = new DateTime(2024, 09, 04, 13, 00, 00), EndTime = new DateTime(2024, 09, 04, 16, 00, 00),
                    Description = "Code-along demo session on creating tables in MySQL" },
                new Activity {
                    Module = modules[0], ActivityType = ActivityType.Assignment, Name = "Query syntax review",
                    StartTime = new DateTime(2024, 09, 07, 09, 00, 00), EndTime = new DateTime(2024, 09, 10, 17, 00, 00),
                    Description = "Review exercises on SQL query syntax (select, where, join)",
                    Documents = [documents[2]] },
                new Activity {
                    Module = modules[1], ActivityType = ActivityType.ELearning, Name = "NoSQL basics",
                    StartTime = new DateTime(2024, 09, 16, 10, 00, 00), EndTime = new DateTime(2024, 09, 16, 10, 00, 00),
                    Description = "Basic differences between relational and non-relational databases" },
                new Activity {
                    Module = modules[3], ActivityType = ActivityType.Lecture, Name = "Introduction",
                    StartTime = new DateTime(2024, 11, 01, 09, 00, 00), EndTime = new DateTime(2024, 11, 01, 10, 30, 00),
                    Description = "Introduction to the course" }
            ];
            await context.AddRangeAsync(activities);

            await context.SaveChangesAsync();
        }
    }
}
