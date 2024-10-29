using CourseLogic.Helper;
using CourseLogic.Interface;
using CourseLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseLogic.Service
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<bool> UserRegisterAsync(StudentDto user)
        {
            user.Id = Guid.NewGuid();

            //檢查帳號重覆
            var stu = await _studentRepository.QueryAsync(user.Email);
            if (stu != null)
            {
                return false;
            }

            //pwd hash
            user.Pwd = PwdHelper.PwdSHA256Hash(user.Pwd, user.Id.ToString());

            //save to db
            await _studentRepository.CreateAsync(user);

            return true;
        }

        public async Task<StudentDto> UserSignAsync(string email, string pwd)
        {
            //驗證帳號是否存在
            var user = await _studentRepository.QueryAsync(email);
            if (user is null)
            {
                return null;
            }

            //驗證密碼
            var hashPwd = PwdHelper.PwdSHA256Hash(pwd, user.Id.ToString());
            if (hashPwd != user.Pwd)
            {
                return null;
            }

            return user;
        }

        public async Task<StudentDto> GetUserAsync(Guid userId)
        {
            return await _studentRepository.QueryAsync(userId);
        }
    }
}
