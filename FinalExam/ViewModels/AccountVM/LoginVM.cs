using System.ComponentModel.DataAnnotations;

namespace FinalExam.ViewModels.AccountVM
{
    public class LoginVM
    {
        [Required]
        public string UsernameorEmail { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
