using LearningProjectASP.Models;

namespace LearningProjectASP.Services
{
    public interface IBlobService
    {
        Task<BlobFile> UploadAsync(BlobFile blob);
        Task<BlobFile?> GetAsync(int id);
        Task<IEnumerable<BlobFile>> GetAllAsync();
        Task<bool> DeleteAsync(int id);
    }
}
