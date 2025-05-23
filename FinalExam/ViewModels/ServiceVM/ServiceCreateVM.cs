using FinalExam.Models;

namespace FinalExam.ViewModels.ServiceVM
{
    public class ServiceCreateVM
    {
        public string Title { get; set; }
        public IFormFile Image { get; set; }
        public string Details { get; set; }
        public int CategoryId { get; set; }
    }
}
