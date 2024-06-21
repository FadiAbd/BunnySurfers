using BunnySurfers.API.Data;
using Microsoft.AspNetCore.Mvc;

namespace BunnySurfers.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatabaseController(LMSDbContext context) : ControllerBase
    {
        private readonly LMSDbContext _context = context;

        // POST: Reset database with seed data
        [HttpPost]
        public async Task<IActionResult> ResetDatabase()
        {
            await _context.Database.EnsureDeletedAsync();
            await _context.Database.EnsureCreatedAsync();
            return NoContent();
        }
    }
}
