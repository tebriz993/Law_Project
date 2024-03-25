using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LawProject.Models
{
    public class CaseStudies
    {
        
        public int Id { get; set; }
        
        public string Image { get; set; }

        [NotMapped]
        public IFormFile Photo { get; set; }

        
        public string Title { get; set; }

        
        public string SubTitle { get; set; }

        
        public string Date { get; set; }

    }
}
