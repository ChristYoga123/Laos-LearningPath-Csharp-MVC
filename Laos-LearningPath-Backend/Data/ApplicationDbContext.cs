using Laos_LearningPath_Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Laos_LearningPath_Backend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<User> users { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<Course> courses { get; set; }
        public DbSet<Lesson> lessons { get; set; }
        public DbSet<Order> orders { get; set; }
    }
}
