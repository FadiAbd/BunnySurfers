using BunnySurfers.API.Entities;

namespace BunnySurfers.API.DTOs
{
    public class UserForStudentViewDTO
    {
        public int UserId { get; set; }
        public UserRole Role { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
    }

    public class DocumentForStudentViewDTO
    {
        public int DocumentId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime TimeOfUpload { get; set; }
        public required string UntrustedFileName { get; set; }

        public UserForStudentViewDTO UploadedBy { get; set; } = null!;
    }

    public class ActivityForStudentViewDTO
    {
        public int ActivityId { get; set; }
        public required string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public ActivityType ActivityType { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int ModuleId { get; set; }

        public List<DocumentForStudentViewDTO> Documents { get; set; } = [];
    }

    public class ModuleForStudentViewDTO
    {
        public int ModuleId { get; set; }
        public required string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public int CourseId { get; set; }

        public List<ActivityForStudentViewDTO> Activities { get; set; } = [];
        public List<DocumentForStudentViewDTO> Documents { get; set; } = [];
    }

    public class CourseForStudentViewDTO
    {
        public int CourseId { get; set; }
        public required string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }

        public List<UserForStudentViewDTO> Users { get; set; } = [];
        public List<ModuleForStudentViewDTO> Modules { get; set; } = [];
        public List<DocumentForStudentViewDTO> Documents { get; set; } = [];
    }
}
