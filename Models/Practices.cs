using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LawProject.Models
{
    public class Practices
    {
        [Key]
        public int Id { get; set; }


        //public string? Image { get; set; }
        //[NotMapped]
        //IFormFile Photo { get; set; }


        [Required]
        public string Title { get; set; }

        [Required]
        public string SubTitle { get; set; }

        public Practices()
        {
            
        }

    }
}
