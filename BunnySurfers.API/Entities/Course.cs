using BunnySurfers.API.Entities;

public class Course
{
    public int CourseId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; } = string.Empty;

    public DateOnly StartDate { get; set; } = DateOnly.FromDateTime(DateTime.Today);
    public DateOnly EndDate { get; set; } = DateOnly.FromDateTime(DateTime.Today);

    public List<User> Users { get; set; } = new List<User>();
    public List<Module> Modules { get; set; } = new List<Module>();
    public List<Document> Documents { get; set; } = new List<Document>();
}
