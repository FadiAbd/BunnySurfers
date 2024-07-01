using AutoMapper;
using BunnySurfers.API.Data;
using BunnySurfers.API.DTOs;
using BunnySurfers.API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BunnySurfers.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ModuleController(LMSDbContext context, IMapper mapper) : ControllerBase
    {
        private readonly LMSDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ModuleGetDTO>>> GetAllModules()
        {
            var modules = await _context.Modules
                .Include(m => m.Documents)
                .Include(m => m.Activities)
                .ThenInclude(a => a.Documents)
                .ToListAsync();
            var moduleDTOs = _mapper.Map<IEnumerable<ModuleGetDTO>>(modules);
            return Ok(moduleDTOs);
        }

        [HttpGet("{moduleId:int}")]
        public async Task<ActionResult<ModuleGetDTO>> GetModule(int moduleId)
        {
            var module = await _context.Modules
                .Include(m => m.Documents)
                .Include(m => m.Activities)
                .ThenInclude(a => a.Documents)
                .FirstOrDefaultAsync(m => m.ModuleId == moduleId);
            if (module is null)
                return NotFound($"A module with ID {moduleId} could not be found");

            var moduleDTO = _mapper.Map<ModuleGetDTO>(module);
            return Ok(moduleDTO);
        }

        [HttpPost]
        public async Task<ActionResult<ModuleGetDTO>> PostModule(ModuleEditDTO moduleDTO)
        {
            var module = _mapper.Map<Module>(moduleDTO);
            _context.Modules.Add(module);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(PostModule), _mapper.Map<ModuleGetDTO>(module));
        }

        [HttpDelete("{moduleId:int}")]
        public async Task<IActionResult> DeleteModule(int moduleId)
        {
            var module = await _context.Modules
                .Include(m => m.Activities)
                .FirstOrDefaultAsync(m => m.ModuleId == moduleId);
            if (module is null)
            {
                return NotFound($"No module with ID {moduleId} was found");
            }

            _context.Modules.Remove(module);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{moduleId:int}")]
        public async Task<IActionResult> PutModule(int moduleId, ModuleEditDTO moduleDTO)
        {
            var module = await _context.Modules.FindAsync(moduleId);
            if (module is null)
                return NotFound($"No module with ID {moduleId} was found");

            // Update module from moduleDTO
            _mapper.Map(moduleDTO, module);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_context.Modules.Find(moduleId) is null)
                {
                    return NotFound($"The module with ID {moduleId} is missing");
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }
    }
}
