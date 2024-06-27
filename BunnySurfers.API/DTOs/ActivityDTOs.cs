using BunnySurfers.API.Entities;

namespace BunnySurfers.API.DTOs
{
    public class ActivityEditDTO
    {
        public required string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public ActivityType ActivityType { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int ModuleId { get; set; }

    }

    public class ActivityGetDTO : ActivityEditDTO
    {
        public int ActivityId { get; set; }
        public List<DocumentGetDTO> Documents { get; set; } = [];
    }
}
