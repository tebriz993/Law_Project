using LawProject.Data;
using LawProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace LawProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly LawProjectDbContext _context;
        public HomeController(LawProjectDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {

            List<Home> home = _context.Home.ToList();
            return View(home);
        }
       

    }
}
