namespace BunnySurfers.API.Entities
{
    public class Teacher : User
    {
        // Teachers can have many courses; courses may have many teachers?
        public List<Course> Courses { get; set; } = [];
    }
}
