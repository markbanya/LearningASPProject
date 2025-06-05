using LearningProjectASP.Models;

namespace LearningProjectASP.Services
{
    public interface ITokenService
    {
        Task<string> GenerateTokenAsync(AppUser user);
    }
}
