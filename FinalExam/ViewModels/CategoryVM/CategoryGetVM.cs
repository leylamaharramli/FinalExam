using System.ComponentModel.DataAnnotations;

namespace FinalExam.ViewModels.CategoryVM
{
    public class CategoryGetVM
    {
        [MinLength(2), MaxLength(10)]
        public string Name { get; set; }
        public int Id { get; set; }
    }
}
