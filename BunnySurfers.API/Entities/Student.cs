namespace BunnySurfers.API.Entities
{
    public class Student : User
    {
        // The Course is nullable in case the student is not currently enrolled in anything
        public int? CourseId { get; set; }
        public Course? Course { get; set; }
    }
}
