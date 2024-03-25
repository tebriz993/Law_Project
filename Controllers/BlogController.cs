using LawProject.Data;
using LawProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace LawProject.Controllers
{
    public class BlogController : Controller
    {
        private readonly LawProjectDbContext _context;
        public BlogController(LawProjectDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Blog> blog = _context.Blog.ToList();
            return View(blog);
        }
    }
}
