using System.Web.Mvc;

namespace PurrfectBlog.Controllers
{
    public class DashboardController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}