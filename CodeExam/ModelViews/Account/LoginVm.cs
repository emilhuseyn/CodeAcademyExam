using System.ComponentModel.DataAnnotations;

namespace CodeExam.ModelViews.Account
{
    public class LoginVm
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
