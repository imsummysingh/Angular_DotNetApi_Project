using CodePulse.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        //constructor for the DbContext
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        //DbSet represents the collection of entities from a particular type, in a relational database
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Category> Categories { get; set; } 
        public DbSet<BlogImage> BlogImages { get; set; }    
    }
}
