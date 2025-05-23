using FinalExam.Models;
using System.ComponentModel.DataAnnotations;

namespace FinalExam.ViewModels.ServiceVM
{
    public class ServiceCreateVM
    {
        [MinLength(5)]
        public string Title { get; set; }
        public IFormFile Image { get; set; }
        public string Details { get; set; }
        public int CategoryId { get; set; }
    }
}
