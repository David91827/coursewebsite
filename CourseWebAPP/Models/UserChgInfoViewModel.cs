using System.ComponentModel.DataAnnotations;
namespace CourseWebAPP.Models
{
    public class UserChgInfoViewModel
    {
        [Required]
        public string Name { get; set; }
        public string Mobile { get; set; }
    }
}
