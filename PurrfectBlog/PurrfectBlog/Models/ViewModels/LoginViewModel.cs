using System.ComponentModel.DataAnnotations;


namespace PurrfectBlog.Models.ViewModels
{
    /// <summary>
    /// View model for the user login form.
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>
        /// Gets or sets the username for login.
        /// </summary>
        [Required(ErrorMessage = "Username is required.")]
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the password for login.
        /// </summary>
        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
