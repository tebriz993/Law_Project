namespace LawProject.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<EmployeeOfClient>? EmployeeOfClients { get; set; }

        public Client()
        {

        }
    }
}
