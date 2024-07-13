using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NhaHangBuffetPBL3.Models;
using NhaHangBuffetPBL3.Repository.IRepository;

namespace NhaHangBuffetPBL3.Areas.Staff.Controllers
{
    [Area("Staff")]
    [Authorize(Roles = "Staff", Policy = "StaffPolicy")]
    public class CreateTableController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateTableController(IUnitOfWork db)
        {
            _unitOfWork = db;
        }
        public IActionResult Index(string? message)
        {
            ViewData["message"] = message;
            if(message=="error")
                ModelState.AddModelError("error", "Bàn đang được sử dụng!");
            else if(message == "success")
                ModelState.AddModelError("success", "Đặt bàn thành công!");
            ViewBag.ListBanAn = _unitOfWork.Table.GetAll().Select(p => p.SeatingId).ToList();//Lay tat ca ban an
            return View();
        }
        public IActionResult CreateTable(int tableId, int amount, string thanhtoan) //Dat Ban
        {
            if (ModelState.IsValid)
            {
                var table = _unitOfWork.Table.GetFirstOrDefault(t => t.SeatingId == tableId);
                if (table.Status != "waiting")
                {
                    return RedirectToAction("Index", new {message="error"});
                }
                Bill hoadon = new Bill
                {
                    BillDate = DateTime.Now,
                    NumberOfPeople = amount,
                    TotalAmount = 100000 * amount,
                    SeatingId = tableId,
                    PaymentType = thanhtoan
                };
                var code = _unitOfWork.Orders.GenerateUniqueCode();
                _unitOfWork.Orders.StorePreOrderCode(code, tableId);
                table.Status = "dang dung";
                _unitOfWork.Table.Update(table);
                _unitOfWork.Bill.Add(hoadon);
                _unitOfWork.Save();
                return RedirectToAction("Index", new { message = "success" });
            }
            return View();
        }
        public IActionResult InHoaDon(int tableId, int amount, string thanhtoan)
        {
            if (ModelState.IsValid)
            {
                var table = _unitOfWork.Table.GetFirstOrDefault(t => t.SeatingId == tableId);
                if (table.Status != "waiting")
                {
                    return RedirectToAction("Index", new { message = "error" });
                }
                Bill hoadon = new Bill
                {
                    BillDate = DateTime.Now,
                    NumberOfPeople = amount,
                    TotalAmount = 100000 * amount,
                    SeatingId = tableId,
                    PaymentType = thanhtoan
                };
                ViewData["Data"] = hoadon;
                var code = _unitOfWork.Orders.GenerateUniqueCode();
                _unitOfWork.Orders.StorePreOrderCode(code, tableId);
                table.Status = "dang dung";
                _unitOfWork.Table.Update(table);
                _unitOfWork.Bill.Add(hoadon);
                _unitOfWork.Save();
                return View("InHoaDon");
            }
            return View("Index");
        }
    }
}
