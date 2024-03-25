using Azure.Core;
using LawProject.Data;
using LawProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LawProject.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly LawProjectDbContext _context;
        public EmployeeController(LawProjectDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Employee> employees = await  _context.Employee.Include(p=>p.Position).ToListAsync();
            return View(employees);
        }
    }
}
