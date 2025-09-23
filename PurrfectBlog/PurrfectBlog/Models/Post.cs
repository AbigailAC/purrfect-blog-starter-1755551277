using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PurrfectBlog.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "All posts require a title between 20 and 200 characters.")]
        [StringLength(200, MinimumLength = 20, ErrorMessage = "Title must be between 20 and 200 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Content body is required.")]
        [StringLength(500, MinimumLength = 20, ErrorMessage = "Post content must be between 20 and 500 characters.")]
        public string Content { get; set; }

        public DateTime CreationDate { get; set; }

        public int AuthorId { get; set; }

        [ForeignKey("AuthorId")]
        public virtual Author Author { get; set; }
    }
}