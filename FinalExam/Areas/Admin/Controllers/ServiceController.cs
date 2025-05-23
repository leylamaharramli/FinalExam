using FinalExam.DataAccessLayer;
using FinalExam.Models;
using FinalExam.ViewModels.CategoryVM;
using FinalExam.ViewModels.ServiceVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalExam.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ServiceController(FinalDbContext _context) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var services = await _context.Services.Select(x => new ServiceGetVM
            {
                Id = x.Id,
                Title = x.Title,
                Details = x.Details,
                ImageUrl = x.ImageUrl,
                CategoryId = x.CategoryId,
                Category = new()
                {
                    Id = x.Category.Id,
                    Name = x.Category.Name
                }
            }).ToListAsync();
            return View(services);
        }
        public async Task<IActionResult> Create()
        {
            await ViewBags();
            return View();
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue || id < 1) return BadRequest();
            var entity = await _context.Services.FirstOrDefaultAsync(x => id == x.Id);
            if (entity is null) return NotFound();
            _context.Services.Remove(entity);
            if (System.IO.File.Exists(Path.Combine("wwwroot", "images")))
                System.IO.File.Delete(Path.Combine("wwwroot", "images"));
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int? id)
        {
            await ViewBags();
            if (!id.HasValue || id < 1) return BadRequest();
            var entity = await _context.Services.FirstOrDefaultAsync(x => id == x.Id);
            if (entity is null) return NotFound();
            ServiceUpdateVM vm = new()
            {
                Id = entity.Id,
                Title = entity.Title,
                Details = entity.Details,
                ImageUrl = entity.ImageUrl,
                CategoryId = entity.CategoryId
            };
            return View(vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ServiceCreateVM vm)
        {
            await ViewBags();
            if (!ModelState.IsValid) return BadRequest();
            if(vm.Image.Length / 1024 > 100)
            {
                ModelState.AddModelError("Image", "File size can not be greater than 100kb");
                return View(vm);
            }
            if (!vm.Image.ContentType.Contains("image"))
            {
                ModelState.AddModelError("Image", "File must be in image format");
                return View(vm);
            }

            string newfilename = Guid.NewGuid().ToString() + vm.Image.FileName;
            string path = Path.Combine("wwwroot", "images", newfilename);
            using FileStream fs = new(path, FileMode.OpenOrCreate);
            await vm.Image.CopyToAsync(fs);



            Service service = new()
            {
                CategoryId = vm.CategoryId,
                ImageUrl = newfilename,
                Title = vm.Title,
                Details = vm.Details,
            };
            await _context.Services.AddAsync(service);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, ServiceUpdateVM vm)
        {
            await ViewBags();
            if (!id.HasValue || id < 1) return BadRequest();
            var entity = await _context.Services.FirstOrDefaultAsync(x => id == x.Id);
            if (entity is null) return NotFound();
            if(vm.Image is not null)
            {
                if (vm.Image.Length / 1024 > 100)
                {
                    ModelState.AddModelError("Image", "File size can not be greater than 100kb");
                    return View(vm);
                }
                if (!vm.Image.ContentType.Contains("image"))
                {
                    ModelState.AddModelError("Image", "File must be in image format");
                    return View(vm);
                }
                string newfilename = Guid.NewGuid().ToString() + vm.Image.FileName;
                string path = Path.Combine("wwwroot", "images", newfilename);
                using FileStream fs = new(path, FileMode.OpenOrCreate);
                await vm.Image.CopyToAsync(fs);

                if (System.IO.File.Exists(Path.Combine("wwwroot", "images")))
                    System.IO.File.Delete(Path.Combine("wwwroot", "images"));

                entity.ImageUrl = newfilename;
            }
            entity.Id = vm.Id;
            entity.CategoryId = vm.CategoryId;
            entity.Title = vm.Title;
            entity.Details = vm.Details;

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private async Task ViewBags()
        {
            var category = await _context.Categories.Select(x => new CategoryGetVM
            {
                Id=x.Id,
                Name=x.Name
            }).ToListAsync();
            ViewBag.Categories = category;
        }
    }
}
