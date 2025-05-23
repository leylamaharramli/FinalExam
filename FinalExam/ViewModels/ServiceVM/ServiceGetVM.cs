using FinalExam.Models;

namespace FinalExam.ViewModels.ServiceVM
{
    public class ServiceGetVM
    {
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string Details { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int Id { get; set; }
    }
}
