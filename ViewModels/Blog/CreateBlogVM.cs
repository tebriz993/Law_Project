namespace LawProject.ViewModels.Blog
{
    public class CreateBlogVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<int>? CategoryIds { get; set; }
        public CreateBlogVM()
        {
            
        }
    }
}
