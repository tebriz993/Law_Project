namespace LawProject.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public Profession Profession { get; set; }
        public int PrefessionId { get; set; }

        public Person()
        {
            
        }
    }
}
