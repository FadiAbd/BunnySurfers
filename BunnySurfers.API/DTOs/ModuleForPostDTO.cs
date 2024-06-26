namespace BunnySurfers.API.DTOs
{
    public class ModuleForPostDTO
    {
        public required string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }

        // A module belongs to a single course
        public int CourseId { get; set; }
    }
}
