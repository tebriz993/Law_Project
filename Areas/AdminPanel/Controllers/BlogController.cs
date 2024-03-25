using LawProject.Data;
using LawProject.ViewModels.Blog;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LawProject.Models;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace LawProject.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class BlogController : Controller
    {
        public readonly LawProjectDbContext _context;
        public BlogController(LawProjectDbContext context)
        {
            _context = context;

        }

        public async Task<IActionResult> Index()
        {
            var blog = await _context.Blog
                .Include(b => b.BlogCategories)
                .ThenInclude(b => b.Category)
                .ToListAsync();

            return View(blog);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _context.Categories.ToListAsync();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateBlogVM blogVM)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _context.Categories.ToListAsync();
                return View();
            }
            Blog blog = new Blog
            {
                Title=blogVM.Title,
                BlogCategories=new List<BlogCategory>(),
            };

            if (blogVM.CategoryIds is null)
            {
                ViewBag.Categories = await _context.Categories.ToListAsync();
                ModelState.AddModelError("CategoryIds", "Bele kategoriya sec");
            }

            foreach (var catid in blogVM.CategoryIds)
            {
                bool result = await _context.Categories.AnyAsync(b => b.Id == catid);
                if (!result)
                {
                    ViewBag.Categories =await _context.Categories.ToListAsync();
                    ModelState.AddModelError("CategoryIds", "Bele kategoriya yoxdur");
                    return View();
                }
                BlogCategory blogCat = new BlogCategory
                {
                    CategoryId = catid,
                    Blog = blog,
                };

                blog.BlogCategories.Add(blogCat);
            }

            await _context.Blog.AddAsync(blog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        
        }
        
        public async Task<IActionResult> Update(int? id)
        {
            if (id is null || id <= 0) return BadRequest();

            Blog blog = await _context.Blog.Where(b => b.Id == id)
                .Include(b => b.BlogCategories)
                .ThenInclude(b => b.Category)
                .FirstOrDefaultAsync();

            if(blog is null) return NotFound();

            UpdateBlogVM updateVM = new UpdateBlogVM
            {
                Title = blog.Title,
                CategoryIds = blog.BlogCategories.Select(b => b.CategoryId).ToList(),
            };

            ViewBag.Categories = await _context.Categories.ToListAsync();
            return View(updateVM);
        }


        [HttpPost]
        public async Task<IActionResult> Update(int? id,UpdateBlogVM updateVM)
        {
            if (id is null || id <= 0) return BadRequest();

            Blog oldblog = await _context.Blog.Where(b => b.Id == id)
                .Include(b => b.BlogCategories)
                .ThenInclude(b => b.Category)
                .FirstOrDefaultAsync();

            if (oldblog is null) return NotFound();
            ViewBag.Categories = await _context.Categories.ToListAsync();

            oldblog.Title = updateVM.Title;

            List<int> createCat = updateVM.CategoryIds.Where(sId => !oldblog.BlogCategories.Exists(b => b.CategoryId == sId)).ToList();
            
            foreach (var catid in createCat)
            {
                bool result = await _context.Categories.AnyAsync(b => b.Id == catid);
                if (!result)
                {
                    ViewBag.Categories = await _context.Categories.ToListAsync();
                    ModelState.AddModelError("CategoryIds", "Bele kategoriya yoxdur");
                    return View(updateVM);
                }
                BlogCategory blogCat = new BlogCategory
                {
                    CategoryId = catid,
                    BlogId = oldblog.Id,
                };

                oldblog.BlogCategories.Add(blogCat);
            }
            List<BlogCategory> blogCategories = oldblog.BlogCategories.Where(b => updateVM.CategoryIds.Any(c => c == b.CategoryId)).ToList();
            _context.BlogCategories.RemoveRange(blogCategories);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

            
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid) return View();

            Blog blog = await _context.Blog
                .Include(b => b.BlogCategories)
                .ThenInclude(b => b.Category)
                .FirstOrDefaultAsync(b=>b.Id==id);

            if (blog == null)
            {
                return View();
            }
            _context.Blog.Remove(blog);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}   
        

    

