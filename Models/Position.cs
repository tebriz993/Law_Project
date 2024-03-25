using System.ComponentModel.DataAnnotations;
using LawProject.Models;

namespace LawProject.Models
{
    public class Position
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Employee>? Employee { get; set; }

        public Position()
        {
                
        }
    }
}
