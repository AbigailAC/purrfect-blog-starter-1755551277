using System;
using PurrfectBlog.Models;
using System.Web.Mvc;
using System.Linq;
using System.Data.Entity;

namespace PurrfectBlog.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize]
    public class PostController : Controller
    {
        private readonly BlogDbContext _context = new BlogDbContext();

        [AllowAnonymous]
        public ActionResult Index()
        {
            var posts = _context.Posts
                    .Include(p => p.Author)
                    .OrderByDescending(p => p.CreationDate)
                    .ToList();
            return View(posts);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            var post = _context.Posts.Include(p => p.Author).SingleOrDefault(p => p.Id == id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
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
                    return RedirectToAction("Details", new { id = post.Id });
                }
                else
                {
                    ModelState.AddModelError("", "Oops! An error occured. Unable to associate post with a valid author. If this continues happening, please contact us");
                }
               
            }
            return View(post);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
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