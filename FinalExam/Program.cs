using FinalExam.DataAccessLayer;
using FinalExam.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace FinalExam
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<FinalDbContext>(opt =>
            {
                opt.UseSqlServer(builder.Configuration.GetConnectionString("MSSQL"));
            });
            builder.Services.AddIdentity<User, IdentityRole<Guid>>(x =>
            {
                x.SignIn.RequireConfirmedEmail = false;
                x.Lockout.MaxFailedAccessAttempts = 3;
                x.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(30);
                x.User.RequireUniqueEmail = true;
                x.Password.RequireNonAlphanumeric = true;
                x.Password.RequireUppercase = true;
                x.Password.RequiredLength = 7;
            }).AddEntityFrameworkStores<FinalDbContext>().AddDefaultTokenProviders();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
          );
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
