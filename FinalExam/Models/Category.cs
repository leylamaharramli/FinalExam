using System.ComponentModel.DataAnnotations;

namespace FinalExam.Models
{
    public class Category:BaseEntity
    {
        [MinLength(2), MaxLength(10)]
        public string Name { get; set; }
        public IEnumerable<Service> Services { get; set; }
    }
}
