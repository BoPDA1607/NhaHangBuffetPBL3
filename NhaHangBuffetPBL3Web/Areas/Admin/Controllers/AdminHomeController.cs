using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NhaHangBuffetPBL3.Repository.IRepository;

namespace NhaHangBuffetPBL3.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin", Policy = "AdminPolicy")]
    public class AdminHomeController : Controller
    {
        private readonly IUnitOfWork _context;

        public AdminHomeController(IUnitOfWork context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ViewData["KetCa"] = _context.Bill.Sum(DateOnly.Parse(DateTime.Now.ToString("yyyy-MM-dd")));
            return View();
        }
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Login", new { area = "" });
        }
    }
}
