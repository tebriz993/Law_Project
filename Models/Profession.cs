namespace LawProject.Models
{
    public class Profession
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Person>? Person { get; set; }
        public Profession()
        {
            
        }
    }
}
