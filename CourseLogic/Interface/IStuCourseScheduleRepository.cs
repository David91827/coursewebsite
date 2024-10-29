using CourseLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseLogic.Interface
{
    public interface IStuCourseScheduleRepository
    {
        public Task<bool> AddScheduleAsync(Guid userId, Guid courseScheduleId);
        public Task<bool> IsExistScheduleAsync(Guid userId, Guid courseScheduleId);
        public Task<List<StuCourseScheduleModel>> GetScheduleAsync(Guid userId);
        public Task<bool> DeleteScheduleAsync(Guid stuScheduleId);
    }
}
