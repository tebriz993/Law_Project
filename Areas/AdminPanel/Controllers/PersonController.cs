using LawProject.Data;
using LawProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LawProject.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class PersonController : Controller
    {
        public readonly LawProjectDbContext _context;
        

        public PersonController(LawProjectDbContext context)
        {
            _context = context;
           
        }

        public async Task<IActionResult> Index()
        {
            List<Person> person = await _context.Person
                .Include(p => p.Profession)
                .ToListAsync();

            return View(person);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Profession = await _context.Profession.ToListAsync();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Person person)
        {
            bool result = await _context.Profession.AnyAsync(p=>p.Id==person.PrefessionId);

            if (!result)
            {
                ModelState.AddModelError("ProfessionId", "Bele vezife yoxdur");
                ViewBag.Profession = await _context.Profession.ToListAsync();
                return View();
            }
            await _context.Person.AddAsync(person);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Update(int Id)
        {
            if(Id==null || Id < 1)
            {
                return BadRequest();
            }

            Person exit = await _context.Person.FirstOrDefaultAsync(p => p.Id == Id);
            if (exit == null)
            {
                return NotFound();
            }
            ViewBag.Profession = await _context.Profession.ToListAsync();


            return View(exit);

        }

        [HttpPost]
        public async Task<IActionResult> Update(int Id, Person person)
        {
            if (Id == null || Id < 1)
            {
                return BadRequest();
            }

            Person exit = await _context.Person.FirstOrDefaultAsync(p => p.Id == Id);
            if (exit == null)
            {
                return NotFound();
            }

            bool result = await _context.Profession.AnyAsync(p => p.Id == person.PrefessionId);
            if (!result)
            {
                ModelState.AddModelError("ProfessionId", "Bele vezife yoxdur");
                ViewBag.Profession = await _context.Profession.ToListAsync();
                return View();
            }

            exit.Name = person.Name;
            exit.Surname = person.Surname;
            exit.PrefessionId = person.PrefessionId;


            
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Delete(int Id)
        {
            if (!ModelState.IsValid) return View();

            Person person = await _context.Person.FirstOrDefaultAsync(p => p.Id == Id);

            if (person == null) return NotFound();

            _context.Person.Remove(person);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
