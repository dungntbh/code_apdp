using ManagerSIMS.Data;
using ManagerSIMS.Facade;
using ManagerSIMS.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Thời gian tồn tại của session
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.SameSite = SameSiteMode.Strict; // Đảm bảo bảo mật khi chia sẻ cookie
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Yêu cầu HTTPS
});

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserFacade, UserFacade>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<ICourseFacade, CourseFacade>();

builder.Services.AddControllersWithViews();
var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();  // ✅ Phải đặt trước UseSession
app.UseSession();  // ✅ Kích hoạt session
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
