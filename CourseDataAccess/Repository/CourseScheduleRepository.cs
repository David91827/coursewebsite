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
    public class CourseScheduleRepository : ICourseScheduleRepository
    {
        private KhNetCourseContext _dbContext;
        public CourseScheduleRepository(KhNetCourseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<CourseScheduleViewModel>> QueryAsync(CourseScheduleQueryCondition? courseScheduleQueryCondition)
        {
            var query = from cs in _dbContext.Courseschedules
                        join c in _dbContext.Courses on cs.Courseid equals c.Id
                        join t in _dbContext.Teachers on cs.Teacherid equals t.Id
                        select new CourseScheduleViewModel()
                        {
                            CourseCode = c.Code,
                            CourseName = c.Name,
                            CourseDesc = c.Description,
                            CourseTimes = c.Times,
                            Edate = cs.Edate,
                            Id = cs.Id,
                            Location = cs.Location,
                            Sdate = cs.Sdate,
                            TeacherName = t.Name
                        };

            if (courseScheduleQueryCondition != null)
            {
                if (!string.IsNullOrEmpty(courseScheduleQueryCondition.CourseName))
                {
                    query = query.Where(c => c.CourseName.Contains(courseScheduleQueryCondition.CourseName));
                }
                if (!string.IsNullOrEmpty(courseScheduleQueryCondition.TeacherName))
                {
                    query = query.Where(t => t.TeacherName.Contains(courseScheduleQueryCondition.TeacherName));
                }
            }


            return await query.ToListAsync();
        }
    }
}
