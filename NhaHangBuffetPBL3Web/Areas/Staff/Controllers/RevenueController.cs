using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NhaHangBuffetPBL3.Repository.IRepository;

namespace NhaHangBuffetPBL3.Areas.Staff.Controllers
{
    [Area("Staff")]
    [Authorize(Roles = "Staff", Policy = "StaffPolicy")]
    public class RevenueController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public RevenueController(IUnitOfWork db)
        {
            _unitOfWork = db;
        }
        public IActionResult Index()
        {
            ViewData["KetCa"] = _unitOfWork.Bill.Sum(DateOnly.Parse(DateTime.Now.ToString("yyyy-MM-dd")));
            return View();
        }
    }
}
