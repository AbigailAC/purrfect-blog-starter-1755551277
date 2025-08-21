using System.Data.Entity;

namespace PurrfectBlog.Models
{
    public class BlogDbContext : DbContext
    {
        public BlogDbContext() : base("DefaultConnection") { }
        public DbSet<Author> Authors { get; set; }
    }
}