namespace BunnySurfers.API.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }

        // All users can upload documents
        public List<Document> Documents { get; set; } = [];
    }
}
