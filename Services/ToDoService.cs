using LearningProjectASP.Data;
using LearningProjectASP.Models;
using Microsoft.EntityFrameworkCore;

namespace LearningProjectASP.Services
{
    public class ToDoService : IToDoService
    {
        private readonly AppDbContext _db;

        public ToDoService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<ToDoItem>> GetAllAsync() =>
            await _db.ToDoItems.ToListAsync();

        public async Task<ToDoItem> CreateAsync(ToDoItem item)
        {
            _db.ToDoItems.Add(item);
            await _db.SaveChangesAsync();
            return item;
        }

        public async Task<bool> CompleteAsync(int id)
        {
            var item = await _db.ToDoItems.FindAsync(id);
            if (item == null)
            {
                return false;
            }

            item.IsCompleted = true;
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var item = await _db.ToDoItems.FindAsync(id);
            if (item == null)
            {
                return false;
            }

            _db.ToDoItems.Remove(item);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
