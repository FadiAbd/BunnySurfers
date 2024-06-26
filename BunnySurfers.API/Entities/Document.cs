namespace BunnySurfers.API.Entities
{
    public class Document
    {
        public int DocumentId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime TimeOfUpload { get; set; }

        // Need to save the given, potentially insecure filename
        // and the actual path to where *we* saved the file
        // https://learn.microsoft.com/en-us/aspnet/core/mvc/models/file-uploads?view=aspnetcore-8.0
        public required string UntrustedFileName { get; set; }
        public required string LocalFilePath { get; set; }

        // Which user uploaded the document?
        public int UserId { get; set; }
        public User UploadedBy { get; set; } = null!;

        // Navigation properties
        // These are nullable, but exactly *one* of them should be non-null
        public int? CourseId { get; set; } = null;
        public int? ModuleId { get; set; } = null;
        public int? ActivityId { get; set; } = null;
    }
}
