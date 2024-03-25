using System.ComponentModel.DataAnnotations;

namespace LawProject.Models
{
    public class Home
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Image { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Subtitle { get; set; }
    }
}
