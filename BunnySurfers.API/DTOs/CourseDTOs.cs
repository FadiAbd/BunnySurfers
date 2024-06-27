namespace BunnySurfers.API.DTOs
{
    public class CourseEditDTO
    {
        public required string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
    }

    public class CourseGetDTO : CourseEditDTO
    {
        public int CourseId { get; set; }
        public List<UserGetDTO> Users { get; set; } = [];
        public List<ModuleGetDTO> Modules { get; set; } = [];
        public List<DocumentGetDTO> Documents { get; set; } = [];
    }
}
