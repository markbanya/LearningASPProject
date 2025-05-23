using LearningProjectASP.Models;
using Microsoft.EntityFrameworkCore;

namespace LearningProjectASP.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<ToDoItem> ToDoItems { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
