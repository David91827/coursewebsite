using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseLogic.Models;

namespace CourseLogic.Interface
{
    public interface IStudentRepository
    {
        Task<bool> CreateAsync(StudentDto user);
        Task<StudentDto> QueryAsync(string email);
        Task<StudentDto> QueryAsync(Guid userId);
        Task<bool> UpdatePwdAsync(Guid userId, string newPwd);
        Task<bool> UpdateInfoAsync(UserInfoReqModel userInfoReqModel);
    }
}
