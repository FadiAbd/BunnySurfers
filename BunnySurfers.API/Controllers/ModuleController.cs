using AutoMapper;
using BunnySurfers.API.Data;
using BunnySurfers.API.DTOs;
using BunnySurfers.API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BunnySurfers.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ModuleController : ControllerBase
    {
        private readonly LMSDbContext _context;
        private readonly IMapper _mapper;

        public ModuleController(LMSDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("{moduleId:int}")]
        public async Task<ActionResult<Module>> Get(int moduleId)
        {
            Module module = await _context.Modules.FindAsync(moduleId);
            if (module == null)
            {
                return NotFound();
            }
            _context.Entry(module).Collection(m => m.Activities).Load();

            return Ok(module);
        }

        [HttpPost]
        public async Task<ActionResult<Module>> PostModule(ModuleForPostDTO moduleDTO)
        {
            var module = _mapper.Map<Module>(moduleDTO);
            _context.Modules.Add(module);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { moduleId = module.ModuleId }, module);
        }

        [HttpDelete("{moduleId:int}")]
        public async Task<IActionResult> DeleteModule(int moduleId)
        {
            var module = await _context.Modules.FindAsync(moduleId);
            if (module == null)
            {
                return NotFound($"No module with ID {moduleId} was found");
            }

            _context.Modules.Remove(module);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{moduleId:int}")]
        public async Task<IActionResult> PutModule(int moduleId, ModuleForPostDTO moduleDTO)
        {
            var module = await _context.Modules.FindAsync(moduleId);
            if (module == null)
            {
                return NotFound($"No module with ID {moduleId} was found");
            }

            // Update module from moduleDTO
            module.Name = moduleDTO.Name;
            module.Description = moduleDTO.Description;
            module.StartDate = moduleDTO.StartDate;
            module.EndDate = moduleDTO.EndDate;
            module.CourseId = moduleDTO.CourseId;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_context.Modules.Find(moduleId) == null)
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
