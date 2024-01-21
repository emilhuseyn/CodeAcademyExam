namespace CodeExam.Models.Base
{
    public class BaseAuditable:BaseEntity
    {
        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; }
    }
}
