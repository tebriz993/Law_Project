using Microsoft.AspNetCore.Mvc;

namespace LawProject.Controllers
{
    public class SingleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
