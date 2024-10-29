using CourseLogic.Interface;
using CourseLogic.Models;
using CourseLogic.Service;
using CourseWebAPP.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Claims;

namespace CourseWebAPP.Controllers
{
    [Authorize]
    public class MemberController : Controller
    {
        private IMemberService _memberService;
        private readonly IStudentService _studentService;
        public MemberController(IMemberService memberService, IStudentService studentService)
        {
            _memberService = memberService;
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var stu = await _studentService.GetUserAsync(userId);
            
            return View(new UserChgInfoViewModel() { Name = stu.UserName, Mobile = stu.Mobile });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(UserChgInfoViewModel userChgInfoViewModel)
        {
            ViewBag.Info = string.Empty;
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (ModelState.IsValid)
            {
                var result = await _memberService.MemberInfoUpdateAsync(
                      new UserInfoReqModel()
                      {
                          UserId = userId,
                          Mobile = userChgInfoViewModel.Mobile,
                          Name = userChgInfoViewModel.Name
                      });
                ViewBag.Info = result ? "更新成功" : "更新失敗";
            }
            return View(userChgInfoViewModel);
        }

        [HttpGet]
        public IActionResult ChangePwd()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePwd(UserChgPwdViewModel userChgPwdViewModel)
        {
            if (ModelState.IsValid)
            {
                //2次密碼是否相符
                if (userChgPwdViewModel.NewPwd != userChgPwdViewModel.ConfirmNewPwd)
                {
                    return View(userChgPwdViewModel);
                }

                var result = await _memberService.MemberPwdUpdateAsync(
                            new UserPwdReqModel()
                            {
                                OldPwd = userChgPwdViewModel.OldPwd,
                                NewPwd = userChgPwdViewModel.NewPwd,
                                UserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier))
                            });
                if (result)
                    return RedirectToAction("Logout", "Login");

                ViewBag.Info = "密碼變更失敗";
                return View(userChgPwdViewModel);
            }

            return View(userChgPwdViewModel);
        }
    }
}
