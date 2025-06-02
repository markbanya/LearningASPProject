using LearningProjectASP.Models;

namespace LearningProjectASP.Services
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
