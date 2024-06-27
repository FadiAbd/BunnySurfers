namespace BunnySurfers.API.DTOs
{
    public class DocumentEditDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }

    public class DocumentGetDTO : DocumentEditDTO
    {
        public int DocumentId { get; set; }
        public DateTime TimeOfUpload { get; set; }
        public required string UntrustedFileName { get; set; }
        public required string LocalFilePath { get; set; }
        public int UserId { get; set; }
    }
}
