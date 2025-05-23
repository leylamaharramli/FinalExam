namespace FinalExam.Models
{
    public class Service:BaseEntity
    {
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string Details { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
