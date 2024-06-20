using BunnySurfers.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace BunnySurfers.API.Data
{
    public class LMSDbContext(DbContextOptions<LMSDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Document> Documents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Provide examples of each entity
            User[] users = [
                new User { UserId = 1, Role = UserRole.Teacher, Name = "Lara Ren"     , Email = "lara.ren@lexicon.se"     },
                new User { UserId = 2, Role = UserRole.Student, Name = "Elle Eve"     , Email = "elle.eve@hotmail.com"    },
                new User { UserId = 3, Role = UserRole.Student, Name = "Stuart Dent"  , Email = "stuart.dent@hotmail.com" },
                new User { UserId = 4, Role = UserRole.Student, Name = "Karl Karlsson", Email = "karlkarl@gmail.com"      },
                new User { UserId = 5, Role = UserRole.None   , Name = "Uno Thorized" , Email = "hackerman@tpu.org"       }
            ];

            Course[] courses = [
                new Course {
                    CourseId = 1, Name = "Databasteknik", StartDate = new DateOnly(2024, 09, 01), EndDate = new DateOnly(2024, 10, 15),
                    Description = "Teknik och verktyg att jobba med både relational databas (SQL) och non-relational databas (NoSQL)" },
                new Course {
                    CourseId = 2, Name = "Entity Framework Core", StartDate = new DateOnly(2024, 11, 01), EndDate = new DateOnly(2024, 12, 15),
                    Description = "Koppla kunskap från Databasteknik till en Object-Relational Mapper med Entity Framework Core" }
            ];

            Module[] modules = [
                new Module {
                    ModuleId = 1, CourseId = courses[0].CourseId, Name = "Relational databas",
                    StartDate = new DateOnly(2024, 09, 01), EndDate = new DateOnly(2024, 09, 15),
                    Description = "Relational DataBase Management Systems (RDBMS) som SQL; ACID; normalized tables; queries and performance" },
                new Module {
                    ModuleId = 2, CourseId = courses[0].CourseId, Name = "Non-relational databas",
                    StartDate = new DateOnly(2024, 09, 16), EndDate = new DateOnly(2024, 09, 30),
                    Description = "NoSQL systems; document data stores; graph databases; design trade-offs with ACID" },
                new Module {
                    ModuleId = 3, CourseId = courses[0].CourseId, Name = "Distributed databas",
                    StartDate = new DateOnly(2024, 10, 01), EndDate = new DateOnly(2024, 10, 15),
                    Description = "Distributed and cloud architecture; transactions and consistency" },
                new Module {
                    ModuleId = 4, CourseId = courses[1].CourseId, Name = "Introduction",
                    StartDate = new DateOnly(2024, 11, 01), EndDate = new DateOnly(2024, 11, 01),
                    Description = "" }
            ];

            Activity[] activities = [
                new Activity {
                    ActivityId = 1, ModuleId = modules[0].ModuleId, ActivityType = ActivityType.Lecture, Name = "Introduction",
                    StartTime = new DateTime(2024, 09, 01, 10, 30, 00), EndTime = new DateTime(2024, 09, 01, 12, 00, 00),
                    Description = "Introduction to the course" },
                new Activity {
                    ActivityId = 2, ModuleId = modules[0].ModuleId, ActivityType = ActivityType.Practice, Name = "Hands-on with MySQL",
                    StartTime = new DateTime(2024, 09, 04, 13, 00, 00), EndTime = new DateTime(2024, 09, 04, 16, 00, 00),
                    Description = "Code-along demo session on creating tables in MySQL" },
                new Activity {
                    ActivityId = 3, ModuleId = modules[0].ModuleId, ActivityType = ActivityType.Assignment, Name = "Query syntax review",
                    StartTime = new DateTime(2024, 09, 07, 09, 00, 00), EndTime = new DateTime(2024, 09, 10, 17, 00, 00),
                    Description = "Review exercises on SQL query syntax (select, where, join)" },
                new Activity {
                    ActivityId = 4, ModuleId = modules[1].ModuleId, ActivityType = ActivityType.ELearning, Name = "NoSQL basics",
                    StartTime = new DateTime(2024, 09, 16, 10, 00, 00), EndTime = new DateTime(2024, 09, 16, 10, 00, 00),
                    Description = "Basic differences between relational and non-relational databases" },
                new Activity {
                    ActivityId = 5, ModuleId = modules[3].ModuleId, ActivityType = ActivityType.Lecture, Name = "Introduction",
                    StartTime = new DateTime(2024, 11, 01, 09, 00, 00), EndTime = new DateTime(2024, 11, 01, 10, 30, 00),
                    Description = "Introduction to the course" }
            ];

            Document[] documents = [
                new Document {
                    DocumentId = 1, Name = "Kursschema", Description = "Schedule", UntrustedFileName = "kursschema.pdf", LocalFilePath = "kursschema.pdf",
                    UserId = users[0].UserId, TimeOfUpload = new DateTime(2024, 08, 01, 09, 00, 00),
                    CourseId = courses[0].CourseId },
                new Document {
                    DocumentId = 2, Name = "Getting started", Description = "Instructions to set up MySQL", UntrustedFileName = "set up mysql.docx", LocalFilePath = "set_up_mysql.docx",
                    UserId = users[0].UserId, TimeOfUpload = new DateTime(2024, 08, 02, 10, 00, 00),
                    ModuleId = modules[0].ModuleId },
                new Document {
                    DocumentId = 3, Name = "Assignment 1", Description = "Submission of assignment 1", UntrustedFileName = "hw1.sh", LocalFilePath = "hw1_elle_eve.txt",
                    UserId = users[1].UserId, TimeOfUpload = new DateTime(2024, 09, 10, 13, 00, 00),
                    ActivityId = activities[2].ActivityId },
            ];

            // Add each of these to their respective tables
            modelBuilder.Entity<User>().HasData(users);
            modelBuilder.Entity<Course>().HasData(courses);
            modelBuilder.Entity<Module>().HasData(modules);
            modelBuilder.Entity<Activity>().HasData(activities);
            modelBuilder.Entity<Document>().HasData(documents);

            // Add the users to the course
            // Since this is a many-to-many operation, the syntax is a bit special
            modelBuilder.Entity<Course>()
                .HasMany(c => c.Users)
                .WithMany(u => u.Courses)
                .UsingEntity(entity => entity
                    .HasData([
                        new { CoursesCourseId = courses[0].CourseId, UsersUserId = users[0].UserId },
                        new { CoursesCourseId = courses[0].CourseId, UsersUserId = users[1].UserId },
                        new { CoursesCourseId = courses[0].CourseId, UsersUserId = users[2].UserId },
                        new { CoursesCourseId = courses[0].CourseId, UsersUserId = users[3].UserId },
                        new { CoursesCourseId = courses[1].CourseId, UsersUserId = users[0].UserId },
                    ]));
        }
    }
}
