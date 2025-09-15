using PurrfectBlog.Models;
using System.Linq;
using System.Web.Mvc;

namespace PurrfectBlog.Controllers
{
    public class AuthController : Controller
    {
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
    }
}