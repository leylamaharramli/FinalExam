using FinalExam.DataAccessLayer;
using FinalExam.Models;
using FinalExam.ViewModels.CategoryVM;
using FinalExam.ViewModels.ServiceVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalExam.Areas.Admin.Controllers
{
    [Area("Admin")]
   
    public class CategoryController(FinalDbContext _context) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var categories = await _context.Categories.Select(x => new CategoryGetVM
            {
                Id = x.Id,
                Name=x.Name
            }).ToListAsync();
            return View(categories);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue || id < 1) return BadRequest();
            var entity = await _context.Categories.FirstOrDefaultAsync(x => id == x.Id);
            if (entity is null) return NotFound();
            _context.Categories.Remove(entity);
            
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (!id.HasValue || id < 1) return BadRequest();
            var entity = await _context.Categories.FirstOrDefaultAsync(x => id == x.Id);
            if (entity is null) return NotFound();
            CategoryUpdateVM vm = new()
            {
                Id = entity.Id,
                Name=entity.Name
            };
            return View(vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryCreateVM vm)
        {
            if (!ModelState.IsValid) return BadRequest();
            

            Category category = new()
            {
                Name=vm.Name
            };
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, CategoryUpdateVM vm)
        {
            if (!id.HasValue || id < 1) return BadRequest();
            var entity = await _context.Categories.FirstOrDefaultAsync(x => id == x.Id);
            if (entity is null) return NotFound();
            
            entity.Id = vm.Id;
            entity.Name = vm.Name;


            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
