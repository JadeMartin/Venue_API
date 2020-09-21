using Microsoft.EntityFrameworkCore;

namespace API.Models {
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

        public DbSet<Category> Category { get; set;}
        public DbSet<Review> Review { get; set;}
        public DbSet<User> User { get; set;}
        public DbSet<Venue> Venue { get; set;}
    }
}
