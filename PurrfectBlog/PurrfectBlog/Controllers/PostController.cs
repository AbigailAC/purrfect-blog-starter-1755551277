using System;
using PurrfectBlog.Models;
using System.Web.Mvc;
using System.Linq;

namespace PurrfectBlog.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private readonly BlogDbContext _context = new BlogDbContext();
        // GET: Post/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Post/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Post post)
        {

            if (ModelState.IsValid)
            {
                var username = User.Identity.Name;
                var author = _context.Authors.FirstOrDefault(a => a.UserName == username);

                if (author != null)
                {
                    post.AuthorId = author.Id;
                    post.CreationDate = DateTime.Now;

                    _context.Posts.Add(post);
                    _context.SaveChanges();
                    // implement redirect to the post pages
                }
                else
                {
                    ModelState.AddModelError("", "Cannot Ffind an author for the current user");
                }
               
            }
            return View(post);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}