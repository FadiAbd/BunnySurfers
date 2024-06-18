using BunnySurfers.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace BunnySurfers.API.Data
{
    public class LMSDbContext(DbContextOptions<LMSDbContext> options) : DbContext(options)
    {
    }
}
