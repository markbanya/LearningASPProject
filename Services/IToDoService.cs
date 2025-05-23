using LearningProjectASP.Models;

namespace LearningProjectASP.Services
{
    public interface IToDoService
    {
        Task<IEnumerable<ToDoItem>> GetAllAsync();
        Task<ToDoItem> CreateAsync(ToDoItem item);
        Task<bool> MarkCompleteAsync(int id);
        Task<bool> DeleteAsync(int id);
    }
}
