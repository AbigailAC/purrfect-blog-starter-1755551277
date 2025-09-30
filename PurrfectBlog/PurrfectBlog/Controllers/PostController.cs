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
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Post post)
        {

            if (ModelState.IsValid)
            {
                var username = User.Identity.Name;
                var author = _context.Authors.FirstOrDefault(a => a.UserName.Equals(username, StringComparison.OrdinalIgnoreCase));

                if (author != null)
                {
                    post.AuthorId = author.Id;
                    post.CreationDate = DateTime.Now;

                    _context.Posts.Add(post);
                    _context.SaveChanges();
                    //TODO: implement redirect to the post details page (likely in next step)
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Cannot find an author for the current user");
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