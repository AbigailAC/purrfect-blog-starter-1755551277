using System;
using PurrfectBlog.Models;
using System.Web.Mvc;
using System.Linq;
using System.Data.Entity;

namespace PurrfectBlog.Controllers
{
    /// Controller responsible for managing blog posts, including listing, viewing details, creating posts, and associating posts with authors.
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

        /// Displays the details of a specific blog post identified by its ID.
        /// </summary>
        /// <param name="id">The unique identifier of the blog post to display. If null, a BadRequest result is returned.</param>
        /// <returns>
        /// Returns the details view for the specified post if found; otherwise, returns a BadRequest or NotFound result.
        /// </returns>
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

        /// Handles the creation of a new blog post. If the model is valid and an author is found,
        /// the post is saved and the user is redirected to the details page. Otherwise, displays validation errors.
        /// </summary>
        /// <param name="post">The post data submitted by the user.</param>
        /// <returns>
        /// Redirects to the details page of the newly created post if successful; otherwise, returns the view with validation errors.
        /// </returns>
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
                    ModelState.AddModelError("", "Oops! An error occurred. Unable to associate post with a valid author. If this continues happening, please contact us");
                }
               
            }
            return View(post);
        }

        /// Releases the unmanaged resources used by the controller and optionally disposes of the managed resources.
        /// </summary>
        /// <param name="disposing">True to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
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