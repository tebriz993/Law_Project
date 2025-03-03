﻿using System.ComponentModel.DataAnnotations;

namespace LawProject.Models
{
    public class About
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Image { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Subtitle1 { get; set; }

        [Required]
        public string Subtitle2 { get; set; }

    }
}
