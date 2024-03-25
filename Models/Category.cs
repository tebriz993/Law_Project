namespace LawProject.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<BlogCategory>? BlogCategories { get; set; }
        public Category()
        {

        }
    }
}
