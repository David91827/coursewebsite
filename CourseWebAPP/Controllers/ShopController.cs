using CourseLogic.Interface;
using CourseWebAPP.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CourseWebAPP.Controllers
{
    [Authorize]
    public class ShopController : Controller
    {
        private IShopService _shopService;
        public ShopController(IShopService shopService)
        {
            _shopService = shopService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _shopService.BookingListAsync(Guid.Parse(userId));

            var vm = new List<StuCourseScheduleViewModel>();
            foreach (var item in result)
            {
                vm.Add(new StuCourseScheduleViewModel
                {
                    Id = item.Id,
                    CourseName = item.CourseName,
                    TeacherName = item.TeacherName,
                    CourseDate = $"{item.Sdate} ~ {item.Edate}"
                });
            }

            return View(vm);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> ShopCar(string scheduleid)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!string.IsNullOrEmpty(scheduleid))
            {
                var result = await _shopService.BookingCourseAsync(Guid.Parse(userId), Guid.Parse(scheduleid));
                if (!result)
                {
                    TempData["ShopInfo"] = "登記失敗，檢查是否重覆登記課程";
                }
                else
                {
                    TempData["ShopInfo"] = "課程登記成功";
                }
            }

            return RedirectToAction("index", "home");
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> DelBooking(string scheduleid)
        {
            await _shopService.BookingDeleteAsync(Guid.Parse(scheduleid));

            return RedirectToAction("index");
        }
    }
}
