using LearningProjectASP.Data;
using LearningProjectASP.Models;
using Microsoft.EntityFrameworkCore;

namespace LearningProjectASP.Services
{
    public class BlobService : IBlobService
    {
        private readonly AppDbContext _db;

        public BlobService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<BlobFile> UploadAsync(BlobFile blob)
        {
            blob.Created = DateTime.UtcNow;
            blob.Updated = DateTime.UtcNow;
            _db.Blobs.Add(blob);
            await _db.SaveChangesAsync();
            return blob;
        }

        public async Task<BlobFile?> GetAsync(int id) =>
            await _db.Blobs.FindAsync(id);

        public async Task<IEnumerable<BlobFile>> GetAllAsync() =>
            await _db.Blobs.ToListAsync();

        public async Task<bool> DeleteAsync(int id)
        {
            var blob = await _db.Blobs.FindAsync(id);
            if (blob == null)
            {
                return false;
            }

            _db.Blobs.Remove(blob);
            await _db.SaveChangesAsync();
            return true;
        }
    }

}
