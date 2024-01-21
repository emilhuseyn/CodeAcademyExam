using CodeExam.Models.Base;

namespace CodeExam.Models
{
    public class Chef:BaseAuditable
    {
        public string Fullname { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
    }
}
