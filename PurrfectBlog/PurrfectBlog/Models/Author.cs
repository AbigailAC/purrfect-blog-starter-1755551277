﻿using System.ComponentModel.DataAnnotations;

namespace PurrfectBlog.Models
{
    public class Author
    {   
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 50 characters.")]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password hash is required.")]
        public string HashedPassword { get; set; }
    }
}