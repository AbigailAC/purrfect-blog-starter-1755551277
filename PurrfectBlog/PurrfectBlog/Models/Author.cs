using System.ComponentModel.DataAnnotations;

namespace PurrfectBlog.Models
{
    public class Author
    {   
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string UserName { get; set; }

        [Required]
        public string HashedPassword { get; set; }
    }
}