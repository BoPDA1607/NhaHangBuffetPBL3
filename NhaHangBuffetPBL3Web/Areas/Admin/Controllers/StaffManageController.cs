using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NhaHangBuffetPBL3.Repository;
using NhaHangBuffetPBL3.DataAccess.Data;
using NhaHangBuffetPBL3.Models;
using NhaHangBuffetPBL3.Repository.IRepository;


namespace NhaHangBuffetPBL3.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin", Policy = "AdminPolicy")]
    public class StaffManageController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public StaffManageController(IUnitOfWork db)
        {
            _unitOfWork = db;
        }
        public IActionResult Index()
        {
            var nhanvien = _unitOfWork.Staff.GetAll();  
            return View(nhanvien);
        }

        // GET: NhanVien/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhanVien = _unitOfWork.Staff
                .GetFirstOrDefault(m => m.Id == id);
            if (nhanVien == null)
            {
                return NotFound();
            }

            return View(nhanVien);
        }

        // GET: NhanVien/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: NhanVien/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,date_of_birth,Username,Password")] Models.Staff nhanVien)
        {
            if (ModelState.IsValid)
            {
                nhanVien.Password = BCrypt.Net.BCrypt.HashPassword(nhanVien.Password);
                _unitOfWork.Staff.Add(nhanVien);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(nhanVien);
        }

        // GET: NhanVien/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhanVien = _unitOfWork.Staff.Find(id);
            if (nhanVien == null)
            {
                return NotFound();
            }
            return View(nhanVien);
        }

        // POST: NhanVien/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,date_of_birth,Username,Password")] Models.Staff nhanVien)
        {
            if (id != nhanVien.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var model = _unitOfWork.Staff.Find(id);
                    if (nhanVien.Password.ToString().CompareTo(model.Password.ToString()) != 0)
                    {
                        model.Password = BCrypt.Net.BCrypt.HashPassword(nhanVien.Password);
                    }
                    model.Name = nhanVien.Name;
                    model.date_of_birth = nhanVien.date_of_birth;
                    model.Username = nhanVien.Username;
                    _unitOfWork.Staff.Update(model);
                    _unitOfWork.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NhanVienExists(nhanVien.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(nhanVien);
        }

        // GET: NhanVien/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhanVien = _unitOfWork.Staff
                .GetFirstOrDefault(m => m.Id == id);
            if (nhanVien == null)
            {
                return NotFound();
            }

            return View(nhanVien);
        }

        // POST: NhanVien/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nhanVien = _unitOfWork.Staff.Find(id);
            if (nhanVien != null)
            {
                _unitOfWork.Staff.Remove(nhanVien);
            }

            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool NhanVienExists(int id)
        {
            return _unitOfWork.Staff.Any(e => e.Id == id);
        }
    }
}
