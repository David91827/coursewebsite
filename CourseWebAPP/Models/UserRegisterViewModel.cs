using System.ComponentModel.DataAnnotations;

namespace CourseWebAPP.Models
{
    public class UserRegisterViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "請輸入名稱")]
        [Display(Name = "名稱")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "請輸入Email做為登入帳號")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "請輸入密碼")]
        [Display(Name = "密碼")]
        public string PassWord { get; set; }

        [Required(ErrorMessage = "請再次輸入密碼")]
        [Display(Name = "再次確認密碼")]
        public string ConfirmPwd { get; set; }
       
    }
}
