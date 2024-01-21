namespace CodeExam.Areas.admin.ViewModel.Chef
{
    public class CreateChefVm
    {
        public string Fullname { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
    }
}
