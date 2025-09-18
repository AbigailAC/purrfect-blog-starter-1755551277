using System.Web.Mvc;

namespace PurrfectBlog.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}