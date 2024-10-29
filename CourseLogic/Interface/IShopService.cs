using CourseLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseLogic.Interface
{
    public interface IShopService
    {
        public Task<bool> BookingCourseAsync(Guid userId, Guid courseScheduleId);
        public Task<List<StuCourseScheduleModel>> BookingListAsync(Guid userId);
        public Task<bool> BookingDeleteAsync(Guid stuScheduleId);
    }
}
