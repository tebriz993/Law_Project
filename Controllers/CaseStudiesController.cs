using LawProject.Data;
using LawProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace LawProject.Controllers
{
    public class CaseStudiesController : Controller
    {
        private readonly LawProjectDbContext _context;
        public CaseStudiesController(LawProjectDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<CaseStudies> caseStudies = _context.CaseStudies.ToList();
            return View(caseStudies);
        }
    }
}
