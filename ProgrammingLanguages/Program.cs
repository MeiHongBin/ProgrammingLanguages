using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using ProgrammingLanguages.Data;
using ProgrammingLanguages.middleware_test;
using ProgrammingLanguages.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//連接字串定義
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
var LanguageProjectconnectionString = 
    builder.Configuration.GetConnectionString("LanguageProject") ?? throw new InvalidOperationException("Connection string 'LanguageProject' not found.");

//新增至DI容器內
builder.Services.AddDbContext<ApplicationDbContext>(options =>options.UseSqlServer(connectionString));
builder.Services.AddDbContext<LanguageProjectContext>(Options => Options.UseSqlServer(LanguageProjectconnectionString));
//註冊必需的服務，包括驗證（Authentication）和授權（Authorization）
builder.Services.AddAuthentication("Cookies")
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login"; // 登入頁面
        options.LogoutPath = "/Account/Logout"; // 登出頁面
        //options.AccessDeniedPath = "/Account/AccessDenied"; // 無權限頁面
    });
builder.Services.AddAuthorization();

//這會擷取可使用 Entity Framework 移轉解析的資料庫相關例外狀況。 發生這些例外狀況時，會產生 HTML 回應，其中包含可能解決問題的動作詳細資料。
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//基礎身分驗證
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

//就是mvc模型生成必要程式
//AddControllersWithViews可參考https://blog.csdn.net/WuLex/article/details/118919396
builder.Services.AddControllersWithViews();

//middleware中介軟體
var app = builder.Build();

// Configure the HTTP request pipeline.配置 HTTP 請求管道
if (app.Environment.IsDevelopment())//環境是否為開發
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    //錯誤時導向/Home/Error
    app.UseHsts();
    //開啟 HSTS（HTTP Strict Transport Security），這是一種安全功能，強制瀏覽器以 HTTPS 通信，避免中間人（Man-in-the-Middle Attack, MITM）攻擊。
}

app.UseHttpsRedirection();//瀏覽http->自動https
app.UseStaticFiles();//靜態文件存放資料夾，預設www.root
app.UseRouting();//啟用URL Routing
app.UseAuthentication();//身分驗證
app.UseAuthorization();//授權
app.UseMiddleware<RequestTimingMiddleware>();//測試middleware用，位置:middleware_test\RequireRunning_time.cs
//可裝中間層驗證更多東西app.UseMiddleware<>{ }
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
    //pattern: "Home/{id?}",
    //defaults: new { controller = "Home", action = "Index" }
    );
//這一段是使用area的部份，原先Home/Index會變成admin/Home/Index，使用此網址呼叫時會導入其他網頁
//app.MapControllerRoute(
//    name: "area",
//    pattern: "{area:admin}/{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();//會將 Razor Pages 的端點新增至 IEndpointRouteBuilder(將路由產生的意思)。

app.Run();
