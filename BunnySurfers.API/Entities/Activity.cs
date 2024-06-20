namespace BunnySurfers.API.Entities
{
    public class Activity
    {
        public int ActivityId { get; set; }
        public required string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public ActivityType ActivityType { get; set; }

        // Activities occur at certain times, not just dates; need DateTime
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        // An activity belongs to one module
        public int ModuleId { get; set; }
        public Module Module { get; set; } = null!;

        public List<Document> Documents { get; set; } = [];
    }
}
