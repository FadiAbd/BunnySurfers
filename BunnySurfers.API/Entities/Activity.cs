namespace BunnySurfers.API.Entities
{
    public class Activity
    {
        public int ActivityId { get; set; }
        public required string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public ActivityType ActivityType { get; set; }

        // Activities occur at certain times, not just dates; need DateTime
        // Not all activities have an associated date
        public DateTime StartTime { get; set; }
        // Not all activities have two times, e.g. due date of an assignment
        public DateTime? EndTime { get; set; }

        // An activity belongs to one module
        public int ModuleId { get; set; }
        public Module Module { get; set; } = null!;

        public List<Document> Documents { get; set; } = [];
    }
}
