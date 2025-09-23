using PurrfectBlog.Models;
using PurrfectBlog.Models.ViewModels;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;


namespace PurrfectBlog.Controllers
{
    public class AuthController : Controller
    {
        [HttpGet]
        public ActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using ( var db = new BlogDbContext())
            {
                if (db.Authors.Any(author => author.UserName.ToLower() == model.UserName.ToLower()))
                {
                    ModelState.AddModelError("UserName", "Sorry, this username is pawlready taken!");
                    return View(model);
                }

                var hashedUserPassword = System.Web.Helpers.Crypto.HashPassword(model.Password);

                var newAuthor = new Author
                {
                    UserName = model.UserName,
                    HashedPassword = hashedUserPassword
                };
                db.Authors.Add(newAuthor);
                db.SaveChanges();
            }

            return RedirectToAction("Login", "Auth");
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using (var db = new BlogDbContext())
            {
                var author = db.Authors.FirstOrDefault(a => a.UserName.ToLower() == model.UserName.ToLower());

                if (author == null )
                {
                    ModelState.AddModelError("", "Username is invalid.");
                    return View(model);
                }

                if (!System.Web.Helpers.Crypto.VerifyHashedPassword(author.HashedPassword, model.Password))
                {
                    ModelState.AddModelError("", "Password is invalid.");
                    return View(model);
                }

                FormsAuthentication.SetAuthCookie(author.UserName, false);
                return RedirectToAction("Index", "Dashboard");
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}