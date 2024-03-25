using LawProject.Models;
using LawProject.Data;
using Microsoft.AspNetCore.Mvc;

namespace LawProject.Controllers
{

    public class AboutController : Controller
    {

        private readonly LawProjectDbContext _context;
        public AboutController(LawProjectDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            About? about = _context.About.FirstOrDefault();
            return View(about);
        }
    }

    
}
