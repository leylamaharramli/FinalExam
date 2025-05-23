using System.ComponentModel.DataAnnotations;

namespace FinalExam.ViewModels.AccountVM
{
    public class RegisterVM
    {
        [MinLength(4), MaxLength(10)]
        [Required]
        public string Username { get; set; }
        public string Fullname { get; set; }
        [DataType(DataType.EmailAddress)]
        [Required]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        [Required]
        public string RepeatPassword { get; set; }
    }
}
