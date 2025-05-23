using System.ComponentModel.DataAnnotations;

namespace FinalExam.ViewModels.CategoryVM
{
    public class CategoryCreateVM
    {
        [MinLength(2), MaxLength(10)]
        public string Name { get; set; }
    }
}
