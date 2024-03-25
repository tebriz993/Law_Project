namespace LawProject.Models
{
    public class EmployeeOfClient
    {
        public int Id { get; set; }

        public Employee Employee { get; set; }
        public int EmployeeId { get; set; }

        public Client Client { get; set; }
        public int ClientId { get; set; }

        public EmployeeOfClient()
        {
            
        }
    }
}
