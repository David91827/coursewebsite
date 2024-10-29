using CourseDataAccess.Models;
using CourseDataAccess.Repository;
using CourseLogic.Interface;
using CourseLogic.Service;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CourseWebAPP
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<KhNetCourseContext>(
                options=> options.UseSqlServer(builder.Configuration.GetConnectionString("KhNetCourseDB"))
                );

            builder.Services.AddScoped<ICourseScheduleService, CourseScheduleService>();
            builder.Services.AddScoped<ICourseScheduleRepository, CourseScheduleRepository>();
            builder.Services.AddScoped<IStudentService, StudentService>();
            builder.Services.AddScoped<IStudentRepository, StudentRepository>();
            builder.Services.AddScoped<IMemberService, MemberService>();
            builder.Services.AddScoped<IStuCourseScheduleRepository, StuCourseScheduleRepository>();
            builder.Services.AddScoped<IShopService, ShopService>();


            //設定 Cookie Authentication（Cookie 身份驗證）
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(option =>
                {
                    option.LoginPath = new PathString("/Login/index"); //自定義登入
                    option.LogoutPath = new PathString("/Login/Logout"); //自定義登出
                    option.ExpireTimeSpan = TimeSpan.FromMinutes(30);　//設定 Cookie 的有效期，這裡指定為 30 分鐘
                    option.SlidingExpiration = false; //SlidingExpiration 是一個布林值，當設定為 true 時，每次用戶發送請求時，系統會自動延長 Cookie 的有效期
                });

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
