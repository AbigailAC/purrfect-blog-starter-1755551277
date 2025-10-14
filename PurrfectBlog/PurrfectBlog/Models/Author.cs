using System.ComponentModel.DataAnnotations;

namespace PurrfectBlog.Models
{
    /// <summary>
    /// Represents an author in the blog system.
    /// </summary>
    public class Author
    {   
        /// <summary>
        /// Gets or sets the unique identifier for the author.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the username for the author.
        /// </summary>
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 50 characters.")]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the hashed password for the author.
        /// </summary>
        [Required(ErrorMessage = "Password hash is required.")]
        public string HashedPassword { get; set; }
    }
}