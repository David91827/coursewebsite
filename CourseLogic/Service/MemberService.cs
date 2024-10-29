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
    public class MemberService : IMemberService
    {
        private IStudentRepository _stuRepository;

        public MemberService(IStudentRepository stuRepository)
        {
            _stuRepository = stuRepository;
        }
        public async Task<bool> MemberPwdUpdateAsync(UserPwdReqModel userPwdReqModel)
        {
            //查詢user
            var stu = await _stuRepository.QueryAsync(userPwdReqModel.UserId);
            if (stu == null) return false;

            //檢查舊密碼是否正確
            var hashPwd = PwdHelper.PwdSHA256Hash(userPwdReqModel.OldPwd, userPwdReqModel.UserId.ToString());
            if (hashPwd != stu.Pwd) return false;

            //更新密碼
            var newHashPwd = PwdHelper.PwdSHA256Hash(userPwdReqModel.NewPwd, userPwdReqModel.UserId.ToString());

            await _stuRepository.UpdatePwdAsync(userPwdReqModel.UserId, newHashPwd);
            return true;
        }

        public async Task<bool> MemberInfoUpdateAsync(UserInfoReqModel userInfoReqModel)
        {
            //查詢user
            var stu = await _stuRepository.QueryAsync(userInfoReqModel.UserId);
            if (stu == null) return false;

            stu.UserName = userInfoReqModel.Name;
            stu.Mobile = userInfoReqModel.Mobile;

            await _stuRepository.UpdateInfoAsync(userInfoReqModel);
            return true;
        }
    }
}
