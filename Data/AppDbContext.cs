using LearningProjectASP.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace LearningProjectASP.Data
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<ToDoItem> ToDoItems { get; set; }
        public DbSet<BlobFile> Blobs { get; set; }

    }
}
