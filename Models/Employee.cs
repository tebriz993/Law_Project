using System.ComponentModel.DataAnnotations;
using LawProject.Models;
namespace LawProject.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        
        public string? Image { get; set; }

        
        public string? FullName { get; set; }

        public int PositionId { get; set; }
        public Position Position { get; set; }
        

        public ICollection<EmployeeOfClient>? EmployeeOfClients { get; set; }

        public Employee()
        {
            
        }


    }
}
