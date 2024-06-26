using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BunnySurfers.API.Data;
using BunnySurfers.API.Entities;
using BunnySurfers.API.DTOs;
using AutoMapper;
using BunnySurfers.API.Utilities;

namespace BunnySurfers.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivitiesController(LMSDbContext context, IMapper mapper) : ControllerBase
    {
        private readonly LMSDbContext _context = context;
        private readonly IMapper _mapper = mapper;


        // GET: api/Activities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Activity>>> GetActivities()
        {
            List<Activity> activities = await _context.Activities.ToListAsync();
            var activityDTOs = _mapper.Map<ActivityForGetDTO>(activities);
            return Ok(activityDTOs);
        }

        // GET: api/Activities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ActivityForGetDTO>> GetActivity(int id)
        {
            var activity = await _context.Activities.FindAsync(id);

            if (activity == null)
            {
                return NotFound($"Could not find activity with id {id}.");
            }

            return Ok(_mapper.Map<ActivityForGetDTO>(activity));
        }

        // PUT: api/Activities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActivity(int id, ActivityForPutDTO inputActivity)
        {
            if (!ActivityTypeIsValid(inputActivity.ActivityType))
                return BadRequest(ActivityTypeInvalidErrorMessage(inputActivity.ActivityType));

            Activity? dbActivity = await _context.Activities.FindAsync(id);
            if (dbActivity == null)
                return NotFound($"Could not find activity with id {id}.");
            _mapper.Map(inputActivity, dbActivity);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // POST: api/Activities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Activity>> PostActivity(ActivityForPostDTO inputActivity)
        {
            if (!ActivityTypeIsValid(inputActivity.ActivityType))
                return BadRequest(ActivityTypeInvalidErrorMessage(inputActivity.ActivityType));
            Activity dbActivity = _mapper.Map<Activity>(inputActivity);

            _context.Activities.Add(dbActivity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetActivity", new { id = dbActivity.ActivityId }, dbActivity);
        }

        // DELETE: api/Activities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivity(int id)
        {
            var activity = await _context.Activities.FindAsync(id);
            if (activity == null)
            {
                return NotFound($"Could not find activity with id {id}.");
            }

            _context.Activities.Remove(activity);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        private static bool ActivityTypeIsValid(ActivityType type) =>
            Enum.IsDefined(type);

        private static string ActivityTypeInvalidErrorMessage(ActivityType type) =>
            $"The given ActivityType {type} was not valid. Valid values are: {EnumUtilities.DescribeValidValues<ActivityType>()}";

    }
}
