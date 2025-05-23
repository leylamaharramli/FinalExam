using FinalExam.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FinalExam.DataAccessLayer
{
    public class FinalDbContext :IdentityDbContext<User, IdentityRole<Guid>,Guid>
    {
        public FinalDbContext (DbContextOptions opt) : base(opt) { }
    public DbSet<Category> Categories { get; set; }
        public DbSet<Service> Services { get; set; }
    }
}
