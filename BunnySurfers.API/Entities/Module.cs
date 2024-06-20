namespace BunnySurfers.API.Entities
{
    public class Module
    {
        public int ModuleId { get; set; }
        public required string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }

        // A module belongs to a single course
        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;

        // A module is composed of many activities
        public List<Activity> Activities { get; set; } = [];

        // A module may contain documents belonging to itself
        // (i.e. not to the activities under it)
        public List<Document> Documents { get; set; } = [];
    }
}
