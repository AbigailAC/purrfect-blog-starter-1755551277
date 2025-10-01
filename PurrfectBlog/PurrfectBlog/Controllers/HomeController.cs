using PurrfectBlog.Models;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;

namespace PurrfectBlog.Controllers
{
    public class HomeController : Controller
    {
        private readonly BlogDbContext _context = new BlogDbContext();

        /// Displays the home page with the three most recent blog posts.
        /// </summary>
        /// <returns>A view showing the recent blog posts.</returns>
        public ActionResult Index()
        {
            var recentPosts = _context.Posts
                          .Include(p => p.Author)
                          .OrderByDescending(p => p.CreationDate)
                          .Take(3)
                          .ToList();

            return View(recentPosts);
        }

        /// Displays the About page for the application.
        /// </summary>
        /// <returns>The About view.</returns>
        public ActionResult About()
        {
            ViewBag.Message = "My application description.";

            return View();
        }

        /// Displays the contact page.
        /// </summary>
        /// <returns>The contact view.</returns>
        public ActionResult Contact()
        {
            ViewBag.Message = "My Contact page.";

            return View();
        }

        /// Releases the unmanaged resources used by the HomeController and optionally disposes of the managed resources.
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