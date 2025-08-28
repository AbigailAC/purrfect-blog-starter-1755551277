using PurrfectBlog.Models;
using System.Web.Mvc;

namespace PurrfectBlog.Controllers
{
    public class AuthController : Controller
    {
        public ActionResult Register()
        {
            return View(new RegisterViewModel());
        }
    }
}