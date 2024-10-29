using CourseDataAccess.Models;
using CourseLogic.Interface;
using CourseLogic.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseDataAccess.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private KhNetCourseContext _dbContext;
        public StudentRepository(KhNetCourseContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> CreateAsync(StudentDto user)
        {
            await _dbContext.Students.AddAsync(new Student()
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.UserName,
                Password = user.Pwd
            });

            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<StudentDto> QueryAsync(string email)
        {
            var stu = await _dbContext.Students.FirstOrDefaultAsync(x => x.Email.ToUpper() == email.ToUpper());

            if (stu == null)
            {
                return null;
            }

            return new StudentDto()
            {
                Id = stu.Id,
                Email = stu.Email,
                UserName = stu.Name,
                Pwd = stu.Password
            };
        }

        public async Task<StudentDto> QueryAsync(Guid userId)
        {
            var stu = await _dbContext.Students.FirstOrDefaultAsync(x => x.Id == userId);

            if (stu == null)
            {
                return null;
            }

            return new StudentDto()
            {
                Id = stu.Id,
                Email = stu.Email,
                UserName = stu.Name,
                Pwd = stu.Password,
                Mobile = stu.Mobile
            };
        }

        public async Task<bool> UpdatePwdAsync(Guid userId, string newPwd)
        {
            var entity = await _dbContext.Students.FirstOrDefaultAsync(u => u.Id == userId);
            if (entity == null)
                return false;


            entity.Password = newPwd;
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateInfoAsync(UserInfoReqModel userInfoReqModel)
        {
            var entity = await _dbContext.Students.FirstOrDefaultAsync(u => u.Id == userInfoReqModel.UserId);
            if (entity == null)
                return false;

            entity.Name = userInfoReqModel.Name;
            entity.Mobile = userInfoReqModel.Mobile;

            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
