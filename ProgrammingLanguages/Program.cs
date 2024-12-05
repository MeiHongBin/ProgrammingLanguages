using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using ProgrammingLanguages.Data;
using ProgrammingLanguages.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
var LanguageProjectconnectionString = 
    builder.Configuration.GetConnectionString("LanguageProject") ?? throw new InvalidOperationException("Connection string 'LanguageProject' not found.");
builder.Services.AddDbContext<LanguageProjectContext>(Options => Options.UseSqlServer(LanguageProjectconnectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();//�s��http->�۰�https
app.UseStaticFiles();//�R�A���s���Ƨ��A�w�]www.root
app.UseRouting();//�ҥ�URL Routing
app.UseAuthorization();//���v  �������ҫ�->���v
//�i�ˤ����h���ҧ�h�F��app.UseMiddleware<>{ }
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
