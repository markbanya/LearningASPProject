using System.ComponentModel.DataAnnotations;

namespace LearningProjectASP.Models
{
    public class BlobFile
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime Updated { get; set; } = DateTime.UtcNow;

        [Required]
        public byte[] Data { get; set; } = [];
    }
}
