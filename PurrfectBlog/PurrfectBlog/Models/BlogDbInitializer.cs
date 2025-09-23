using System.Data.Entity;
using System.Collections.Generic;
using System.Web.Helpers;

namespace PurrfectBlog.Models
{
    // We are using DropCreateDatabaseIfModelChanges for simplicity of development, will update to migrations later
    public class BlogDbInitializer : DropCreateDatabaseIfModelChanges<BlogDbContext>
    {
        protected override void Seed(BlogDbContext context)
        {
            var authors = new List<Author>
            {
                new Author { UserName = "Shakespeare", HashedPassword = Crypto.HashPassword("ToBeOrNotToBe123!") },
                new Author { UserName = "Hemingway", HashedPassword = Crypto.HashPassword("TheOldManAndTheSea123!") },
                new Author { UserName = "Herbert" , HashedPassword = Crypto.HashPassword("LisanAlGaib123!") }
            };
            authors.ForEach(a => context.Authors.Add(a));
            context.SaveChanges();
        }
    }
}