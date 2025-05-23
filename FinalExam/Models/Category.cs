namespace FinalExam.Models
{
    public class Category:BaseEntity
    {
        public string Name { get; set; }
        public IEnumerable<Service> Services { get; set; }
    }
}
