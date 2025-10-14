using System.Web.Mvc;
using PurrfectBlog.Models;
using PurrfectBlog.Models.ViewModels;
using System.Linq;
using System.Data.Entity;

namespace PurrfectBlog.Controllers
{
    /// <summary>
    /// Controller responsible for handling the user dashboard functionality.
    /// </summary>
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly BlogDbContext _context = new BlogDbContext();

        /// <summary>
        /// Displays the user dashboard with their posts and statistics.
        /// </summary>
        /// <returns>The dashboard view with user's posts and statistics.</returns>
        public ActionResult Index()
        {
            // Get the current user's username from session or authentication
            var currentUser = User.Identity.Name;
            
            // Find the author by username
            var author = _context.Authors.FirstOrDefault(a => a.UserName == currentUser);
            
            if (author == null)
            {
                // If no author found, return empty dashboard
                return View(new DashboardViewModel());
            }

            // Get user's posts ordered by creation date (newest first)
            var userPosts = _context.Posts
                .Include(p => p.Author)
                .Where(p => p.AuthorId == author.Id)
                .OrderByDescending(p => p.CreationDate)
                .ToList();

            // Create view model
            var viewModel = new DashboardViewModel
            {
                UserPosts = userPosts,
                TotalPosts = userPosts.Count,
                TotalCommentsReceived = 0, // Placeholder - would need comments table
                UserName = author.UserName
            };

            return View(viewModel);
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