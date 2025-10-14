using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PurrfectBlog.Models
{
    /// <summary>
    /// Represents a blog post in the system.
    /// </summary>
    public class Post
    {
        /// <summary>
        /// Gets or sets the unique identifier for the post.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the title of the blog post.
        /// </summary>
        [Required(ErrorMessage = "All posts require a title between 20 and 200 characters.")]
        [StringLength(200, MinimumLength = 20, ErrorMessage = "Title must be between 20 and 200 characters.")]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the content body of the blog post.
        /// </summary>
        [Required(ErrorMessage = "Content body is required.")]
        [StringLength(500, MinimumLength = 20, ErrorMessage = "Post content must be between 20 and 500 characters.")]
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the post was created.
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Gets or sets the foreign key identifier of the author who created this post.
        /// </summary>
        public int AuthorId { get; set; }

        /// <summary>
        /// Gets or sets the navigation property to the author who created this post.
        /// </summary>
        [ForeignKey("AuthorId")]
        public virtual Author Author { get; set; }
    }
}