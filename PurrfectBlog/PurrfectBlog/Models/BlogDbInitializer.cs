using System.Data.Entity;
using System.Collections.Generic;
using System.Web.Helpers;
using System;
using System.Linq;

namespace PurrfectBlog.Models
{
    // We are using DropCreateDatabaseIfModelChanges for simplicity of development, will update to migrations later
    public class BlogDbInitializer : DropCreateDatabaseIfModelChanges<BlogDbContext>
    {
        protected override void Seed(BlogDbContext context)
        {

            if (context.Authors.Any())
            {
                return;
            }

            var authors = new List<Author>
            {
                new Author { UserName = "Shakespeare", HashedPassword = Crypto.HashPassword("ToBeOrNotToBe123!") },
                new Author { UserName = "Hemingway", HashedPassword = Crypto.HashPassword("TheOldManAndTheSea123!") },
                new Author { UserName = "Herbert" , HashedPassword = Crypto.HashPassword("LisanAlGaib123!") }
            };
            authors.ForEach(a => context.Authors.Add(a));
            context.SaveChanges();

            var posts = new List<Post>
            {
                new Post
                {
                    Title = "Hey guys, first time poster! Just found a kitten, what should I feed her" ,
                    Content = "Right meow she's sitting on the couch and cleaning herself. She's a little thing... maybe 5 pounds at most! Found her outside and not sure what to feed her as I can't go to the store for catfood right now.",
                    CreationDate = DateTime.Now,
                    AuthorId = authors[0].Id
                },
                new Post
                {
                    Title = "I'm allergic but still want a cat, what breed makes sense?",
                    Content = "As the title states, I'm actually allergic to cats! But I just find them SOOO cute!! I know there are hypoallergenic breeds, but was wondering if someone could point me to what breeds would make the most sense for me. Thanks!",
                    CreationDate = DateTime.Now,
                    AuthorId = authors[1].Id
                }
            };
            posts.ForEach(p => context.Posts.Add(p));
            context.SaveChanges();

            base.Seed(context);
        }
    }
}