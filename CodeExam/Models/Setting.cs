using CodeExam.Models.Base;

namespace CodeExam.Models
{
    public class Setting:BaseEntity
    {
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Instagram { get; set; }
        
    }
}
