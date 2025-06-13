namespace LearningProjectASP.Dto
{
    public class BlobUploadDto
    {
        public string Name { get; set; } = string.Empty;
        public IFormFile File { get; set; } = default!;
    }
}
