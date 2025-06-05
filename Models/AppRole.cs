using Microsoft.AspNetCore.Identity;

namespace LearningProjectASP.Models
{
    public class AppRole : IdentityRole
    {
        public string Description { get; set; } = string.Empty;
    }
}