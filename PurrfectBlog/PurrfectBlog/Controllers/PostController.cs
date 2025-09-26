using System;
using PurrfectBlog.Models;
using System.Web.Mvc;

namespace PurrfectBlog.Controllers
{
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
                post.CreationDate = DateTime.UtcNow;
                _context.Posts.Add(post);
                _context.SaveChanges();

                return RedirectToAction("Index", "Home");
            }
            return View(post);
        }
    }
}