namespace BunnySurfers.API.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public UserRole Role { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }

        // To accomodate Teachers, a User may have many courses
        // Students belonging to only one course must be handled elsewhere
        public List<Course> Courses { get; set; } = [];

        // Users can upload documents
        public List<Document> Documents { get; set; } = [];
    }
}
