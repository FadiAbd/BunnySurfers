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
    }
}
