using PurrfectBlog.Models;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;

namespace PurrfectBlog.Controllers
{
    public class HomeController : Controller
    {
        private readonly BlogDbContext _context = new BlogDbContext();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var recentPosts = _context.Posts
                          .Include(p => p.Author)
                          .OrderByDescending(p => p.CreationDate)
                          .Take(3)
                          .ToList();

            return View(recentPosts);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult About()
        {
            ViewBag.Message = "My application description.";

            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Contact()
        {
            ViewBag.Message = "My Contact page.";

            return View();
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