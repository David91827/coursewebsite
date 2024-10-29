using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseLogic.Models;

namespace CourseLogic.Interface
{
    public interface IMemberService
    {
        Task<bool> MemberPwdUpdateAsync(UserPwdReqModel userPwdReqModel);
        Task<bool> MemberInfoUpdateAsync(UserInfoReqModel userInfoReqModel);
    }
}
