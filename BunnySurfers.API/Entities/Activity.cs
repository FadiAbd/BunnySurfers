namespace BunnySurfers.API.Entities
{
    public class Activity
    {
        public int ActivityId { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public ActivityType ActivityType { get; set; }

        // Activities may occur at certain times, not just dates
        public DateTime StartDate { get; set; }
        // Not all activities have two dates, e.g. due date of an assignment
        public DateTime? EndDate { get; set; }

        // An activity belongs to one module
        public Module Module { get; set; } = null!;
    }
}
