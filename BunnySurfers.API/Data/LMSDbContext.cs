using BunnySurfers.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace BunnySurfers.API.Data
{
    public class LMSDbContext(DbContextOptions<LMSDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Document> Documents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*
             * Without any additional configuration, add-migration correctly creates:
             * - Courses
             * - Modules
             * - Activities
             * - Courses x Teachers
             * It incorrectly creates:
             * - Users: Additional CourseId column; maybe unnecessary Discriminator column
             * - Documents: Additional nullable columns for Course/Module/Activity Ids
             * It fails to create:
             * - Students
             * - Teachers
             */

            // Establish Students table
            modelBuilder.Entity<Student>()
                .ToTable("Students");

            // Establish Teachers table
            modelBuilder.Entity<Teacher>()
                .ToTable("Teachers");

            // Explicitly creating these two tables fixes the above issues
            // For Documents the extra nullable columns are left in
        }
    }
}
