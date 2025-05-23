using FinalExam.Models;
using FinalExam.ViewModels.AccountVM;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FinalExam.Controllers
{
    public class AccountController(UserManager<User> _userManager, SignInManager<User> _signInManager, RoleManager<IdentityRole<Guid>> _roleManager) : Controller
    {
        public async Task<IActionResult> Register()
        {
            return View();
        }
        public async Task<IActionResult> Login()
        {
            return View();
        }
        public async Task<IActionResult> CreateRoles()
        {
            await _roleManager.CreateAsync(new() { Name = "Admin" });
            await _roleManager.CreateAsync(new() { Name = "User" });
            return Ok("Roles created");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM vm)
        {
            if (!ModelState.IsValid) return BadRequest();
            User user = new()
            {
                Email = vm.Email,
                Fullname = vm.Fullname,
                UserName = vm.Username
            };
            var result = await _userManager.CreateAsync(user, vm.Password);
            if (!result.Succeeded)
            {
                foreach(var error in result.Errors )
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(vm);
            }
            var role = await _userManager.AddToRoleAsync(user, "User");
            return RedirectToAction(nameof(Login));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login (LoginVM vm, string? ReturnUrl)
        {
            if (!ModelState.IsValid) return BadRequest();
            User? user;
            if (vm.UsernameorEmail.Contains("@"))
            {
                user = await _userManager.FindByEmailAsync(vm.UsernameorEmail);
            }
            else
            {
                user = await _userManager.FindByNameAsync(vm.UsernameorEmail);
            }
            if(user is null)
            {
                ModelState.AddModelError("", "Username or Email is incorrect");
                return View(vm);
            }
            var result = await _signInManager.PasswordSignInAsync(user, vm.Password, true, true);
            if (!result.Succeeded)
            {
                if (result.IsLockedOut)
                {
                    ModelState.AddModelError("", "You reached max attempt count");
                    return View(vm);
                }
                else if (result.IsNotAllowed)
                {
                    ModelState.AddModelError("", "You can not sign in. Please contact with admin");
                    return View(vm);
                }
                else
                {
                    ModelState.AddModelError("", "Username or password is incorrect");
                    return View(vm);
                }
            }
            if (ReturnUrl is not null)
                return LocalRedirect(ReturnUrl);
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> CreateAdmin()
        {
            User admin = new()
            {
                Email = "admin@gmail.com",
                UserName = "Admin",
                Fullname = "Admin"
            };
            var result = await _userManager.CreateAsync(admin, "Admin123!");
            var role = await _userManager.AddToRoleAsync(admin, "Admin");
            return Ok("Admin created");
        }
    }
}
