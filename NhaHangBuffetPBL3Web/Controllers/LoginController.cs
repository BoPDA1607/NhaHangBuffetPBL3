using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NhaHangBuffetPBL3.Models.Records;
using System.Security.Claims;

using NhaHangBuffetPBL3.Models;
using NhaHangBuffetPBL3.DataAccess.Data;
using NhaHangBuffetPBL3.Repository.IRepository;

namespace NhaHangBuffet.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUnitOfWork _context;

        public LoginController(IUnitOfWork context)
        {
            _context = context;
        }
        #region Login
        [HttpGet]
        public IActionResult Login(string? ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }
        public async Task<IActionResult> Login(LoginRecord model, string? ReturnUrl)
        {

            if (ModelState.IsValid)
            {
                var nhanvien = _context.Staff.GetFirstOrDefault(
                    nv => nv.Username == model.Username);
                if (nhanvien == null)
                {
                    ModelState.AddModelError("loi", "không có nhân viên này");
                }
                else if (nhanvien.Username != model.Username) { ModelState.AddModelError("loi", "Sai thông tin đăng nhập"); }
                else
                {
                    List<Claim> claims;
                    string role;
                    if (model.Username == "Admin" && BCrypt.Net.BCrypt.Verify(model.Password, nhanvien.Password))
                    {
                        claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, nhanvien.Username),
                            new Claim(ClaimTypes.Role, "Admin")
                        };
                        role = "Admin";
                    }
                    else
                    {
                        claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, nhanvien.Username),
                            new Claim(ClaimTypes.Role,"Staff")
                        };
                        role = "Staff";
                    }

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                                                 claimsPrincipal,
                                                 new AuthenticationProperties
                                                 {
                                                     IsPersistent = false
                                                 });
                    
                    if (role != null && ReturnUrl == null)
                    {
                        if (role == "Staff") ReturnUrl = "/Staff/Staff";
                        else ReturnUrl = "/Admin/AdminHome";
                    }
                    return Redirect(ReturnUrl);
                }
            }
            return View();
        }
        #endregion        
    }
}


