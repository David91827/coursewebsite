using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseLogic.Models;

namespace CourseLogic.Interface
{
    public interface ICourseScheduleRepository
    {
        //Task<IEnumerable<CourseScheduleViewModel>> QueryAsync();
        Task<IEnumerable<CourseScheduleViewModel>> QueryAsync(CourseScheduleQueryCondition? courseScheduleQueryCondition);
    }
}
