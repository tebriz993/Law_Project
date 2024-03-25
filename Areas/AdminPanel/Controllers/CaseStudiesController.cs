using LawProject.Data;
using LawProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Routing.Matching;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.IO;

namespace LawProject.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class CaseStudiesController : Controller
    {
        public readonly LawProjectDbContext _context;
        public readonly IWebHostEnvironment _env;

        public CaseStudiesController(LawProjectDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index()
        {
            List<CaseStudies> caseStudies = _context.CaseStudies.ToList();
            return View(caseStudies);
        }


        public IActionResult Create()
        {

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(CaseStudies caseStudies)
        {
            if (!ModelState.IsValid) return View();
            if (caseStudies == null)
            {
                ModelState.AddModelError("Photo", "Sekil secilmeyib");
                return View();
            }

            if (!caseStudies.Photo.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("Photo", "Sekiil tipi duzgun deyil");
                return View();
            }

            if (caseStudies.Photo.Length / 1024 > 200)
            {
                ModelState.AddModelError("Photo", "Sekil olcusu odemir");
                return View();
            }

            var fileName = Guid.NewGuid().ToString() + "" + caseStudies.Photo.FileName;

            string path = Path.Combine(_env.WebRootPath, "assets/img", fileName);

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await caseStudies.Photo.CopyToAsync(stream);
            }
            await _context.CaseStudies.AddAsync(caseStudies);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
 
        }

        public async Task<IActionResult> Delete(int Id)
        {
            if (Id == null || Id < 1) return BadRequest();

            CaseStudies caseStudies = await  _context.CaseStudies.FirstOrDefaultAsync(s => s.Id == Id);

            if (caseStudies == null) return NotFound();

            string path = Path.Combine(_env.WebRootPath, "img", caseStudies.Image);

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }

            _context.CaseStudies.Remove(caseStudies);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int Id)
        {
            if(Id==null || Id < 1)
            {
                return BadRequest();
            }

            CaseStudies caseStudies = await _context.CaseStudies.FirstOrDefaultAsync(s => s.Id == Id);
            if (caseStudies == null)
            {
                return NotFound();
            }

            return View(caseStudies);
        }

        [HttpPost]
        public async Task<IActionResult> Update(CaseStudies caseStudies)
        {
            if (!ModelState.IsValid) return View();

            CaseStudies old = await _context.CaseStudies.FirstOrDefaultAsync(s => s.Id == caseStudies.Id);

            if(old == null) return NotFound();

            if (caseStudies.Photo != null)
            {
                if (!caseStudies.Photo.ContentType.Contains("image/"))
                {
                    ModelState.AddModelError("Photo", "Sekil tipi duzgun deyil");
                    return View();
                }
                if (caseStudies.Photo.Length / 1024 > 200)
                {
                    ModelState.AddModelError("Photo", "Olcu uygun deyil");
                    return View();
                }
            }

            string oldpath = Path.Combine(_env.WebRootPath, "assets/img", old.Image);
            if (System.IO.File.Exists(oldpath))
            {
                System.IO.File.Delete(oldpath);
            }
            var fileName = Guid.NewGuid().ToString() + " " + caseStudies.Photo.FileName;
            old.Image = fileName;

            string newpath = Path.Combine(_env.WebRootPath, "img", fileName);

            using(FileStream stream=new FileStream(newpath, FileMode.Create))
            {
                await caseStudies.Photo.CopyToAsync(stream);
            }

            old.Title = caseStudies.Title;
            old.SubTitle = caseStudies.SubTitle;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
