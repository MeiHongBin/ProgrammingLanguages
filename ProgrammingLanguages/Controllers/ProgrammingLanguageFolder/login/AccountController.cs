using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;
using ProgrammingLanguages.Models;
using System.Security.Claims;
using ProgrammingLanguages.ProgrammingLanguageModels.LoginPartialVM;

namespace ProgrammingLanguages.Controllers.ProgrammingLanguageFolder.login
{
    public class AccountController : Controller
    {
        //DI容器
        private readonly LanguageProjectContext _context;

        public AccountController(LanguageProjectContext context)
        {
            _context = context;
        }

        //登入頁面
        [HttpGet]
        public IActionResult Login()
        {
            return PartialView("_LoginPartial");
        }
        //登入動作
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            //比較輸入者輸入帳號
            var user = _context.Users.FirstOrDefault(u => u.UserName == username);//如果資料庫有搜尋到第一筆相符的，沒有則返回null
            if (user == null || !VerifyPassword(password, user.PasswordHash))//VerifyPassword:通常用於比較使用者輸入的密碼與資料庫中存儲的密碼雜湊值是否匹配。
            {
                //如果驗證失敗，添加一個錯誤訊息到 ModelState 中，用於向前端頁面傳遞錯誤訊息。
                //""：指定錯誤的鍵，這裡的空字串表示這是一個通用錯誤，而不是特定於某個欄位。
                ModelState.AddModelError("", "無效的帳號或密碼");//將指定的 errorMessageErrors 加入至與指定 key 相關聯之 實例。
                return View();
            }
            // 登入成功，建立身份驗證Claim
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email)
            };
            var identity = new ClaimsIdentity(claims, "Cookies");//身分宣告，指定身份驗證類型為基於 Cookie 的身份驗證。
            var principal = new ClaimsPrincipal(identity);//建立一個主體，表示當前的使用者，並將 ClaimsIdentity 附加到主體上。

            //SignInAsync：將使用者的身份信息（principal）保存到伺服器的 HTTP 上下文中，並使用 Cookie 身份驗證。
            //"Cookies"：指定身份驗證方案，與 ClaimsIdentity 中使用的方案一致。
            await HttpContext.SignInAsync("Cookies", principal);
            return RedirectToAction("Index", "Home"); //重定向至Index
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("Cookies");//登出
            Console.WriteLine("登出成功");
            //return RedirectToAction("Login", "Account");//重定向至Login
            //return RedirectToAction("Index", "Home"); //重定向至Index
            return NoContent();
            //return View();
        }
        //密碼驗證(僅使用，未了解)
        private bool VerifyPassword(string password, string hashedPassword)
        {
            // 實現密碼驗證（通常使用 BCrypt 或類似算法）
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }

    }
}
