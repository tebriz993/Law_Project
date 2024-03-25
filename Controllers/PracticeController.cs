using LawProject.Data;
using LawProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace LawProject.Controllers
{
    public class PracticeController : Controller
    {
        private readonly LawProjectDbContext _context;
        public PracticeController(LawProjectDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Practices> practices = _context.Practices.ToList();
            return View(practices);
        }
    }
}
