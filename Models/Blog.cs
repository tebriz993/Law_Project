using System.ComponentModel.DataAnnotations;

namespace LawProject.Models
{
    public class Blog
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Image { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string CivilLaw { get; set; }

        [Required]
        public string Date { get; set; }

        [Required]
        public string Subtitle { get; set; }


        public List<BlogCategory>? BlogCategories { get; set; }
        public Blog()
        {
            
        }
    }
}
