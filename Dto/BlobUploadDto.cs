namespace LearningProjectASP.Dto
{
    public class BlobUploadDto
    {
        public string Name { get; set; } = string.Empty;
        public string MetadataJson { get; set; } = "{}";
        public IFormFile File { get; set; } = default!;
    }
}
