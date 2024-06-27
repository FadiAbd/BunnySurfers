namespace BunnySurfers.API.DTOs
{
    public class ModuleEditDTO
    {
        public required string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public int CourseId { get; set; }
    }

    public class ModuleGetDTO : ModuleEditDTO
    {
        public int ModuleId { get; set; }
        public List<ActivityGetDTO> Activities { get; set; } = [];
        public List<DocumentGetDTO> Documents { get; set; } = [];
    }
}
