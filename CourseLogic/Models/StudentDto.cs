using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseLogic.Models
{
    public class StudentDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Pwd { get; set; }
        public string Email { get; set; }
        public string? Mobile { get; set; }
    }
}
