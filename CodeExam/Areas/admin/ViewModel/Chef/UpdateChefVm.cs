using CodeExam.Models.Base;

namespace CodeExam.Areas.admin.ViewModel.Chef
{
    public class UpdateChefVm:BaseEntity
    {
        public string Fullname { get; set; }
        public string Description { get; set; }
        public string? ImageUrl { get; set; }
        public IFormFile Image { get; set; }
    }
}
