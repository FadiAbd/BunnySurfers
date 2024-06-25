using BunnySurfers.API.Data;
using BunnySurfers.API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BunnySurfers.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ModuleController(LMSDbContext context) : ControllerBase
    {
        private readonly LMSDbContext _context = context;

        [HttpGet("{ModuleId:int}")]
        public async Task<ActionResult<Module>> Get(int ModuleId)
        {
            Module? module = await _context.Modules.FindAsync(ModuleId);
            if (module == null)
            {
                return NotFound();
            }
            _context.Entry(module).Collection(m => m.Activities).Load();

            return Ok(module);
        }
    }
}
