using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjectDATN.Data.EF;
using ProjectDATN.Data.Entities;

using ProjectDATN.Web.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connectString = builder.Configuration.GetConnectionString("AppDB");
builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    options.UseSqlServer(connectString);
});

builder.Services.AddScoped<IVnPayService, VnPayService>();
builder.Services.AddMvc();

//For cart

builder.Services.AddDistributedMemoryCache();           // Đăng ký dịch vụ lưu cache trong bộ nhớ (Session sẽ sử dụng nó)
builder.Services.AddSession(cfg =>
{                    // Đăng ký dịch vụ Session
    cfg.Cookie.Name = "Cart";             // Đặt tên Session - tên này sử dụng ở Browser (Cookie)
    cfg.IdleTimeout = new TimeSpan(0, 30, 0);    // Thời gian tồn tại của Session
});
// for identity

builder.Services.AddIdentity<AppUser, IdentityRole>()
        .AddEntityFrameworkStores<ApplicationDBContext>()
        .AddDefaultTokenProviders();

/*builder.Services.AddDefaultIdentity<AppUser>()

                 .AddEntityFrameworkStores<ApplicationDBContext>()
                .AddDefaultTokenProviders();
*/
//builder.Services.AddDefaultIdentity<AppUser>()
//    .AddRoles<IdentityRole>()
//    .AddEntityFrameworkStores<ApplicationDBContext>();
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.Password.RequiredLength = 8;
    options.Password.RequireDigit = true;
    options.Password.RequireNonAlphanumeric = false;

    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
    options.Lockout.AllowedForNewUsers = false;

    options.User.AllowedUserNameCharacters = // các ký tự đặt tên user
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = true;  // Email là duy nhất

    options.SignIn.RequireConfirmedEmail = true;
    options.SignIn.RequireConfirmedPhoneNumber = false;
    options.SignIn.RequireConfirmedAccount = true;
});

/*builder.Services.ConfigureApplicationCookie(option =>
{
    option.LoginPath = "/Identity/Signup";
    option.LogoutPath = "/Identity/Logout";
    option.AccessDeniedPath = "/Identity/AccessDenied";
    option.ExpireTimeSpan = TimeSpan.FromHours(10);
});*/

//builder.Services.Configure<MailSetting>(builder.Configuration.GetSection("MailSettings"));
//builder.Services.AddControllersWithViews();

//smtp send email

builder.Services.AddOptions();
var mailsetting = builder.Configuration.GetSection("MailSettings");
builder.Services.Configure<MailSettings>(mailsetting);
builder.Services.AddSingleton<IEmailSender, SendMailService>();


builder.Services.AddNotyf(config =>
{
    config.DurationInSeconds = 3;
    config.IsDismissable = true;
    config.Position = NotyfPosition.TopCenter;
});

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
app.UseSession();
app.UseRouting();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.UseNotyf();

app.MapControllerRoute(
    name: "area",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();