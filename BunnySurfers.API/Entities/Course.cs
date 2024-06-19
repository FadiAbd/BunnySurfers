namespace BunnySurfers.API.Entities
{
    public class Course
    {
        public int CourseId { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }

        // Courses have many students
        // Courses may have many teachers?
        public List<User> Users { get; set; } = [];

        // Courses are composed of many modules
        public List<Module> Modules { get; set; } = [];

        public List<Document> Documents { get; set; } = [];
    }
}
