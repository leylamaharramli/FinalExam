using FinalExam.DataAccessLayer;
using FinalExam.ViewModels;
using FinalExam.ViewModels.CategoryVM;
using FinalExam.ViewModels.ServiceVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalExam.Controllers
{
    public class HomeController(FinalDbContext _context) : Controller
    {
        public async  Task<IActionResult> Index()
        {
            var services = await _context.Services.Select(x => new ServiceGetVM
            {
                Id=x.Id,
                Title=x.Title,
                ImageUrl=x.ImageUrl,
                Details=x.Details,
                CategoryId=x.CategoryId,
                Category= new()
                {
                    Id=x.Category.Id,
                    Name=x.Category.Name
                }
            }).ToListAsync();

            var categories = await _context.Categories.Select(x => new CategoryGetVM
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();

            HomeVM vm = new()
            {
                Services = services,
                Categories = categories
            };
            return View(vm);
        }
    }
}
