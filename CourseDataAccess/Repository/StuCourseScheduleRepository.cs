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
    public class StuCourseScheduleRepository : IStuCourseScheduleRepository
    {
        private KhNetCourseContext _dbContext;
        public StuCourseScheduleRepository(KhNetCourseContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> AddScheduleAsync(Guid userId, Guid courseScheduleId)
        {
            var entity = new Stucourseschedule()
            {
                Id = Guid.NewGuid(),
                Studentid = userId,
                Cscheduleid = courseScheduleId
            };

            await _dbContext.Stucourseschedules.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> IsExistScheduleAsync(Guid userId, Guid courseScheduleId)
        {
            var entity = await _dbContext.Stucourseschedules.FirstOrDefaultAsync(
                   d => d.Studentid == userId
                   && d.Cscheduleid == courseScheduleId);

            return (entity is null ? false : true);
        }

        public async Task<List<StuCourseScheduleModel>> GetScheduleAsync(Guid userId)
        {
            var query = from stusch in _dbContext.Stucourseschedules
                        join coursesch in _dbContext.Courseschedules on stusch.Cscheduleid equals coursesch.Id
                        join c in _dbContext.Courses on coursesch.Courseid equals c.Id
                        join t in _dbContext.Teachers on coursesch.Teacherid equals t.Id
                        where stusch.Studentid == userId
                        select new StuCourseScheduleModel
                        {
                            Id = stusch.Id,
                            CourseName = c.Name,
                            TeacherName = t.Name,
                            Sdate = coursesch.Sdate,
                            Edate = coursesch.Edate
                        };

            return await query.ToListAsync();
        }
       
        public async Task<bool> DeleteScheduleAsync(Guid stuScheduleId)
        {
            var entity = await _dbContext.Stucourseschedules.FirstOrDefaultAsync(s => s.Id == stuScheduleId);

            if (entity != null)
            {
                _dbContext.Stucourseschedules.Remove(entity);
            }
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
