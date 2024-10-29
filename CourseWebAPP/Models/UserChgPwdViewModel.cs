using System.ComponentModel.DataAnnotations;

namespace CourseWebAPP.Models
{
    public class UserChgPwdViewModel
    {
        [Required]
        public string OldPwd { get; set; }
        [Required]
        public string NewPwd { get; set; }
        [Required]
        public string ConfirmNewPwd { get; set; }
    }
}
