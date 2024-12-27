using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;
using ProgrammingLanguages.Models;
using System.Security.Claims;

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

        //登入相關
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            //比較輸入者輸入帳號
            var user = _context.Users.FirstOrDefault(u => u.UserName == username);
            if (user == null || !VerifyPassword(password, user.PasswordHash))
            {
                ModelState.AddModelError("", "無效的帳號或密碼");//將指定的 errorMessageErrors 加入至與指定 key 相關聯之 實例。
                return View();
            }

            // 登入成功，建立身份驗證Claim
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Email, user.Email)
        };
            var identity = new ClaimsIdentity(claims, "Cookies");
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync("Cookies", principal);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("Cookies");
            return RedirectToAction("Login", "Account");
        }

        private bool VerifyPassword(string password, string hashedPassword)
        {
            // 實現密碼驗證（通常使用 BCrypt 或類似算法）
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }

    }
}
