using CodeExam.Models;

namespace CodeExam.ModelViews.Home
{
    public class HomeVm
    {
        public List<Chef> chefs { get; set; }
        public Setting? Setting { get; set; }
    }
}
