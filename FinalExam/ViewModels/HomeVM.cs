using FinalExam.ViewModels.CategoryVM;
using FinalExam.ViewModels.ServiceVM;

namespace FinalExam.ViewModels
{
    public class HomeVM
    {
        public List<CategoryGetVM> Categories { get; set; }
        public List<ServiceGetVM> Services { get; set; }
    }
}
