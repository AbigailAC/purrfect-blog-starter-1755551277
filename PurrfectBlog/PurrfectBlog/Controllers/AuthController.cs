using PurrfectBlog.Models;
using PurrfectBlog.Models.ViewModels;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;


namespace PurrfectBlog.Controllers
{
    /// <summary>
    /// Controller responsible for handling user authentication including registration, login, and logout.
    /// </summary>
    public class AuthController : Controller
    {
        /// <summary>
        /// Displays the user registration form.
        /// </summary>
        /// <returns>The registration view.</returns>
        [HttpGet]
        public ActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        /// <summary>
        /// Handles the user registration form submission.
        /// </summary>
        /// <param name="model">The registration form data.</param>
        /// <returns>Redirects to login page on success, or redisplays form with errors on failure.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using (var db = new BlogDbContext())
            {
                if (db.Authors.Any(author => author.UserName.Equals(model.UserName, StringComparison.OrdinalIgnoreCase)))
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
        /// <summary>
        /// Displays the user login form.
        /// </summary>
        /// <returns>The login view.</returns>
        [HttpGet]
        public ActionResult Login()
        {
            return View(new LoginViewModel());
        }

        /// <summary>
        /// Handles the user login form submission.
        /// </summary>
        /// <param name="model">The login form data.</param>
        /// <returns>Redirects to dashboard on success, or redisplays form with errors on failure.</returns>
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using (var db = new BlogDbContext())
            {
                var author = db.Authors.FirstOrDefault(a => a.UserName.Equals(model.UserName, StringComparison.OrdinalIgnoreCase));

                if (author == null)
                {
                    ModelState.AddModelError("UserName", "Username is invalid.");
                    return View(model);
                }

                if (!System.Web.Helpers.Crypto.VerifyHashedPassword(author.HashedPassword, model.Password))
                {
                    ModelState.AddModelError("Password", "Password is invalid.");
                    return View(model);
                }

                FormsAuthentication.SetAuthCookie(author.UserName, false);
                return RedirectToAction("Index", "Dashboard");
            }
        }

        /// <summary>
        /// Logs out the current user and redirects to the home page.
        /// </summary>
        /// <returns>Redirects to the home page.</returns>
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}