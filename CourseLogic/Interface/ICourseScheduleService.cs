using CourseLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseLogic.Interface
{
    public interface ICourseScheduleService
    {
        //Task<List<CourseScheduleViewModel>> QueryAsync();
        Task<List<CourseScheduleViewModel>> QueryAsync(CourseScheduleQueryCondition? courseScheduleQueryCondition);
    }
}
