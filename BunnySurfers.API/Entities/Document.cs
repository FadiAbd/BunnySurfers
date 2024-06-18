namespace BunnySurfers.API.Entities
{
    public class Document
    {
        // The actual document can be accessed through a separate table
        // with the document filepath linked to this DocumentId
        public int DocumentId { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public DateTime TimeOfUpload { get; set; }

        // Which user uploaded the document?
        public User UploadedBy { get; set; } = null!;
    }
}
