using LearningProjectASP.Models;

namespace LearningProjectASP.Services
{
    public interface IUserService
    {
        Task<List<User>> GetAllAsync();
        Task<User?> GetByIdAsync(int id);
        Task<User> CreateAsync(User user);
        Task<bool> UpdateAsync(int id, User updatedUser);
        Task<bool> DeleteAsync(int id);
    }
}