using System.ComponentModel.DataAnnotations;

namespace CourseWebAPP.Models
{
    public class UserLoginViewModel
    {
        [Required(ErrorMessage = "請輸入Email做為登入帳號")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "請輸入密碼")]
        [Display(Name = "密碼")]
        public string Password { get; set; }
    }
}
