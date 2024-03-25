namespace LawProject.ViewModels.Blog
{
    public class UpdateBlogVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<int>? CategoryIds { get; set; }
        public UpdateBlogVM()
        {
            
        }
    }
}
