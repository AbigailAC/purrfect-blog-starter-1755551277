using System.ComponentModel.DataAnnotations;

namespace PurrfectBlog.Models
{
    public class Author
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string UserName { get; set; }

        public string HashedPassword { get; set; }

        public string Email { get; set; }
    }
}