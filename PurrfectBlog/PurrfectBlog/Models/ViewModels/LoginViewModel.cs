using System.ComponentModel.DataAnnotations;


namespace PurrfectBlog.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Username is required,")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
