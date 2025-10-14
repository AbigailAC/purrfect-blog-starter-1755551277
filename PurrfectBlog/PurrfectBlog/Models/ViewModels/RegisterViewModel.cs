using System.ComponentModel.DataAnnotations;


namespace PurrfectBlog.Models.ViewModels
{
    /// <summary>
    /// View model for the user registration form.
    /// </summary>
    public class RegisterViewModel
    {
        /// <summary>
        /// Gets or sets the username for registration.
        /// </summary>
        [Required(ErrorMessage = "Username is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 50 characters.")]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the password for registration.
        /// </summary>
        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long.")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the password confirmation for registration.
        /// </summary>
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}