using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using NhaHangBuffetPBL3.DataAccess.Data;
using NhaHangBuffetPBL3.Repository.IRepository;
using NhaHangBuffetPBL3.Repository;
using NhaHangBuffetPBL3.Services.VNPay;

namespace NhaHangBuffetPBL3
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<NhaHangBuffetContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("NhaHangBuffetPBL3"));
            });

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddSingleton<IVnPayService, VnPayService>();
            builder.Services.AddDistributedMemoryCache();

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                options.LoginPath = "/Login/Login";
                options.AccessDeniedPath = "/Login/Login";
            });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
                options.AddPolicy("StaffPolicy", policy => policy.RequireRole("Staff"));
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

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "Login",
                pattern: "login/login",
                defaults: new { controller = "Login", action = "Login" });

            app.MapControllerRoute(
                name: "default",
                pattern: "{area=Customers}/{controller=Home}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "Admin",
                pattern: "{area=Admin}/{controller=Home}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "Staff",
                pattern: "{area=Staff}/{controller=Staff}/{action=Profile}/{id?}");

            app.Run();
        }
    }
}