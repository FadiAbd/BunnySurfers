using BunnySurfers.API.Data;
using Microsoft.AspNetCore.Mvc;

namespace BunnySurfers.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DatabaseController(LMSDbContext context) : ControllerBase
    {
        private readonly LMSDbContext _context = context;

        // POST: Clear and re-initialize the database
        [HttpPost]
        public async Task<IActionResult> Clear()
        {
            await SeedData.ClearDatabaseAsync(_context);
            return NoContent();
        }

        // POST: Add seed data to the database
        [HttpPost]
        public async Task<IActionResult> Add()
        {
            await SeedData.AddSeedDataAsync(_context);
            return NoContent();
        }

        // POST: Clear the database and add seed data
        [HttpPost]
        public async Task<IActionResult> Reset()
        {
            await SeedData.ResetDatabaseAsync(_context);
            return NoContent();
        }
    }
}
