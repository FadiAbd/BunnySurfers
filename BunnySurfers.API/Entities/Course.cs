namespace BunnySurfers.API.Entities
{
    public class Course
    {
        public int CourseId { get; set; }
        public required string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }

        // Courses have many students
        // Courses may have many teachers?
        public List<User> Users { get; set; } = [];

        // Courses are composed of many modules
        public List<Module> Modules { get; set; } = [];

        // A course may contain documents belonging to itself
        // (i.e. not to any modules or activities under it)
        public List<Document> Documents { get; set; } = [];
    }
}
