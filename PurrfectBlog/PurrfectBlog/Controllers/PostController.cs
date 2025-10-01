using System;
using PurrfectBlog.Models;
using System.Net;
using System.Web.Mvc;
using System.Linq;
using System.Data.Entity;

namespace PurrfectBlog.Controllers
{
    /// <summary>
    /// Controller responsible for managing blog posts, including listing, viewing details, creating, editing, and deleting posts.
    /// </summary>
    [Authorize]
    public class PostController : Controller
    {
        private readonly BlogDbContext _context = new BlogDbContext();

        // GET: Posts
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
        /// Displays the details of a specific blog post identified by its ID.
        /// </summary>
        /// <param name="id">The unique identifier of the blog post to display.</param>
        /// <returns>The details view for the specified post, or an HTTP error status if not found.</returns>
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var post = _context.Posts.Include(p => p.Author).SingleOrDefault(p => p.Id == id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // GET: Posts/Create
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Handles the server-side processing for creating a new blog post.
        /// </summary>
        /// <param name="post">The post data submitted from the create form.</param>
        /// <returns>A redirect to the new post's details page on success, or redisplays the form with errors on failure.</returns>
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
                    ModelState.AddModelError("", "Oops! An error occurred. Unable to associate post with a valid author.");
                }
            }
            return View(post);
        }

        /// <summary>
        /// Displays the form for editing an existing blog post.
        /// </summary>
        /// <param name="id">The ID of the post to edit.</param>
        /// <returns>The edit view populated with the post's data, or an HTTP error status if not found or not authorized.</returns>
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var post = _context.Posts.Include(p => p.Author).SingleOrDefault(p => p.Id == id);
            if (post == null)
            {
                return HttpNotFound();
            }
            if (post.Author == null || post.Author.UserName != User.Identity.Name)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            return View(post);
        }

        /// <summary>
        /// Handles the server-side processing for updating an existing blog post.
        /// </summary>
        /// <param name="post">The updated post data submitted from the edit form.</param>
        /// <returns>A redirect to the updated post's details page on success, or redisplays the form with errors on failure.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Post post)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(post).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Details", new { id = post.Id });
            }
            return View(post);
        }

        /// <summary>
        /// Displays a confirmation page before deleting a post.
        /// </summary>
        /// <param name="id">The ID of the post to be deleted.</param>
        /// <returns>The delete confirmation view, or an HTTP error status if not found or not authorized.</returns>
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var post = _context.Posts.Include(p => p.Author).SingleOrDefault(p => p.Id == id);
            if (post == null)
            {
                return HttpNotFound();
            }
            // Authorization Check
            if (post.Author == null || post.Author.UserName != User.Identity.Name)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            return View(post);
        }

        /// <summary>
        /// Permanently deletes a post after user confirmation.
        /// </summary>
        /// <param name="id">The ID of the post to delete.</param>
        /// <returns>A redirect to the post index page on success.</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var post = _context.Posts.Include(p => p.Author).SingleOrDefault(p => p.Id == id);
            if (post == null)
            {
                return RedirectToAction("Index");
            }
            if (post.Author == null || post.Author.UserName != User.Identity.Name)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            _context.Posts.Remove(post);
            _context.SaveChanges();
            TempData["SuccessMessage"] = "Post successfully deleted.";
            return RedirectToAction("Index");
        }

        /// <summary>
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