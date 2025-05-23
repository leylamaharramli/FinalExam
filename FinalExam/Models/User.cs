using Microsoft.AspNetCore.Identity;

namespace FinalExam.Models
{
    public class User :IdentityUser<Guid>
    {
        public string Fullname { get; set; }
    }
}
