using FinalExam.Models;
using System.ComponentModel.DataAnnotations;

namespace FinalExam.ViewModels.ServiceVM
{
    public class ServiceGetVM
    {
        [MinLength(5)]
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string Details { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int Id { get; set; }
    }
}
