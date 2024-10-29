using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseLogic.Models;

namespace CourseLogic.Interface
{
    public interface IStudentService
    {
        Task<bool> UserRegisterAsync(StudentDto user);
        Task<StudentDto> UserSignAsync(string email, string pwd);
        Task<StudentDto> GetUserAsync(Guid userId);
    }
}
