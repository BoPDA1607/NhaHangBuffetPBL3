using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NhaHangBuffetPBL3.Models;
using NhaHangBuffetPBL3.Repository.IRepository;

namespace NhaHangBuffetPBL3.Areas.Staff.Controllers
{
    [Area("Staff")]
    [Authorize(Roles = "Staff", Policy = "StaffPolicy")]
    public class StaffController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public StaffController(IUnitOfWork db)
        {
            _unitOfWork = db;
        }
       
        public IActionResult Index()
        {
            ViewData["Ban1"] = _unitOfWork.Menu.GetFoodByMenu(1);
            ViewData["Ban2"] = _unitOfWork.Menu.GetFoodByMenu(2);
            ViewData["Ban3"] = _unitOfWork.Menu.GetFoodByMenu(3);
            ViewData["Ban4"] = _unitOfWork.Menu.GetFoodByMenu(4);
            ViewData["Ban5"] = _unitOfWork.Menu.GetFoodByMenu(5);
            ViewData["Order"] = _unitOfWork.Orders.GetAll();

            return View(_unitOfWork.Table.GetAll());
        }
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login","Login", new { area = "" });
        }
        public IActionResult CompleteOrder(string OrderId, DateTime thoigian)
        {
                var menu = _unitOfWork.Menu.GetFirstOrDefault(m => m.OrderId == OrderId && m.TimeOrder == thoigian);
                if (menu != null)
                {
                    menu.Status = true;
                    _unitOfWork.Menu.Update(menu);
                    _unitOfWork.Save();
                }
            return RedirectToAction("Index");
        }
        public IActionResult DeleteTable(int seatingId)
        {
            var record = _unitOfWork.Table.GetFirstOrDefault(p=>p.SeatingId== seatingId);
            if(record != null)
            {
                if(record.Status=="dang dung")
                {
                    var order = _unitOfWork.Orders.GetRecords(p=>p.SeatingId== seatingId);
                    foreach(var item in order)
                    {
                        if (item.IsUsed == 1)
                        {
                            item.IsUsed = -1;
                            _unitOfWork.Orders.Update(item);
                            break;
                        }
                    }
                    var deleteCartItems = _unitOfWork.Cart.Where(c => c.SeaatingId == seatingId).Select(c => c);
                    foreach (Cart cart in deleteCartItems)
                    {
                        _unitOfWork.Cart.Remove(cart);
                    }
                    record.Status = "waiting";
                    _unitOfWork.Table.Update(record);
                }
                _unitOfWork.Save();
            }
            return RedirectToAction("Index");
        }
    }
}
