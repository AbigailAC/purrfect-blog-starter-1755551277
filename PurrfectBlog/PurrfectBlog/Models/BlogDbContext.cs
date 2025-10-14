using System.Data.Entity;

namespace PurrfectBlog.Models
{
    /// <summary>
    /// Entity Framework database context for the blog application.
    /// </summary>
    public class BlogDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the BlogDbContext class.
        /// </summary>
        public BlogDbContext() : base("DefaultConnection") { }

        /// <summary>
        /// Gets or sets the Authors entity set.
        /// </summary>
        public DbSet<Author> Authors { get; set; }

        /// <summary>
        /// Gets or sets the Posts entity set.
        /// </summary>
        public DbSet<Post> Posts { get; set; }
    }
}