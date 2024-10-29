using CourseLogic.Interface;
using CourseLogic.Models;
using CourseWebAPP.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CourseWebAPP.Controllers
{
    public class LoginController : Controller
    {
        private readonly IStudentService _studentService;

        //DI from ctor
        public LoginController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(UserLoginViewModel userLoginViewModel)
        {
            if (ModelState.IsValid)
            {
                var stu = await _studentService.UserSignAsync(userLoginViewModel.Email, userLoginViewModel.Password);
                if (stu is null)
                {
                    ModelState.AddModelError(string.Empty, "登入失敗");
                    return View(userLoginViewModel);
                }

                //處理 MVC 登入狀態處理
                Claim[] claims = new[] {
                    new Claim(ClaimTypes.Name, stu.UserName)
                    , new Claim(ClaimTypes.NameIdentifier, stu.Id.ToString())
                };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                ClaimsPrincipal principal = new ClaimsPrincipal(claimsIdentity);

                //執行登入
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(principal));
                return RedirectToAction("Index", "Home");
            }

            return View(userLoginViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult UserRegister()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserRegister(UserRegisterViewModel userRegisterModel)
        {
            if (ModelState.IsValid)
            {
                //驗證二次密碼的輸入是否相同
                if (userRegisterModel.PassWord != userRegisterModel.ConfirmPwd)
                {
                    //二次密碼不符
                    ModelState.AddModelError("ConfirmPwd", "確認密碼與密碼不相符");
                    return View(userRegisterModel);
                }

                //call service to create user account
                var result = await _studentService.UserRegisterAsync(
                              new StudentDto()
                              {
                                  Email = userRegisterModel.Email,
                                  UserName = userRegisterModel.UserName,
                                  Pwd = userRegisterModel.PassWord
                              });

                if (!result)
                {
                    ModelState.AddModelError(string.Empty, "註冊失敗(帳號重覆)");
                    return View(userRegisterModel);
                }

                return View("Index");
            }
            return View(userRegisterModel);
        }
    }
}
