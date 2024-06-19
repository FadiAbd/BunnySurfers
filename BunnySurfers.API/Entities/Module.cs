namespace BunnySurfers.API.Entities
{
    public class Module
    {
        public int ModuleId { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }

        // A module belongs to a single course
        public Course Course { get; set; } = null!;

        // A module is composed of many activities
        public List<Activity> Activities { get; set; } = [];

        public List<Document> Documents { get; set; } = [];
    }
}
