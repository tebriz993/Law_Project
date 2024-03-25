using LawProject.Data;
using Microsoft.AspNetCore.Mvc;
using LawProject.Models;
using Microsoft.EntityFrameworkCore;


namespace LawProject.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class PosetionController : Controller
    {
        public readonly LawProjectDbContext _context;
        public PosetionController(LawProjectDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Position> positions = _context.Positions.ToList();
            return View(positions);
        }




        public IActionResult Create()
        {
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Position position)
        {
            if (!ModelState.IsValid) return View();

            bool result = await _context.Positions.AnyAsync(p=>p.Name.Trim().ToLower()==position.Name.Trim().ToLower());
            if (result)
            {
                ModelState.AddModelError("Name","Eyni adda Position artiq movcuddur...");
                return View();
            }
            await _context.Positions.AddAsync(position);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }






        public async Task<IActionResult> Update(int Id)
        {
            if(Id==null || Id < 1)
            {
                return BadRequest();
            }

            Position old = await _context.Positions.FirstOrDefaultAsync(o => o.Id == Id);
            if (old == null)
            {
                return NotFound();
            }

            return View(old);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int Id,Position position)
        {
            if (Id == null || Id < 1)
            {
                return BadRequest();
            }

            Position old = await _context.Positions.FirstOrDefaultAsync(o => o.Id == Id);
            if (old == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (old.Name==position.Name)
            {
                return RedirectToAction(nameof(Index));
            }
            bool result = await _context.Positions.AnyAsync(p => p.Name.Trim().ToLower() == position.Name.Trim().ToLower() && p.Id!=old.Id);
            if (result)
            {
                ModelState.AddModelError("Name", "Bu adda vezife artiq var");
                return View(old);
            }
            old.Name = position.Name;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            
        }




        public async Task<IActionResult> Delete(int? Id)
        {
            if (Id == null || Id < 1)
            {
                return BadRequest();
            }
            Position position = await _context.Positions.FirstOrDefaultAsync(o => o.Id == Id);
            if (position == null) NotFound();

            _context.Positions.Remove(position);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            
        }


    }
}
