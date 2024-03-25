using Microsoft.AspNetCore.Mvc;

namespace LawProject.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
