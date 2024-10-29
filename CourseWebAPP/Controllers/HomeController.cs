using CourseLogic.Interface;
using CourseLogic.Service;
using CourseWebAPP.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using CourseLogic.Models;

namespace CourseWebAPP.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICourseScheduleService _courseScheduleService;

        public HomeController(ILogger<HomeController> logger
            , ICourseScheduleService courseScheduleService)
        {
            _logger = logger;
            _courseScheduleService = courseScheduleService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var vm = await _courseScheduleService.QueryAsync(null);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(string CourseName, string TeacherName)
        {
            ViewBag.CourseName = CourseName;
            ViewBag.TellerName = TeacherName;

            var vm = await _courseScheduleService.QueryAsync(new CourseScheduleQueryCondition()
            {
                CourseName = CourseName,
                TeacherName = TeacherName
            });
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CourseScheduleQuery(string CourseName, string TeacherName)
        {
            ViewBag.CourseName = CourseName;
            ViewBag.TellerName = TeacherName;

            var vm = await _courseScheduleService.QueryAsync(new CourseScheduleQueryCondition()
            {
                CourseName = CourseName,
                TeacherName = TeacherName
            });
            return View("index",vm);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
