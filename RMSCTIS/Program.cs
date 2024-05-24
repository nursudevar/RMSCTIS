using Business.Services;
using DataAccess_.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using MVC.Settings;

var builder = WebApplication.CreateBuilder(args);

#region AppSettings

var section = builder.Configuration.GetSection(nameof(MVC.Settings.AppSettings)); 
                                                                                  
section.Bind(new MVC.Settings.AppSettings()); 
                                              
#endregion


#region Ioc Container
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<Db>(options => options.UseSqlServer(connectionString));

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IResourceService, ResourceService>();


#endregion


#region Authentication
builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)

    .AddCookie(config =>
    {
        config.LoginPath = "/Account/Login";
    
        config.AccessDeniedPath = "/Account/AccessDenied";
     
        config.ExpireTimeSpan = TimeSpan.FromMinutes(AppSettings.CookieExpirationInMinutes);
        
        config.SlidingExpiration = true;
      
    });
#endregion




builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

#region Authentication
app.UseAuthentication();
#endregion

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
