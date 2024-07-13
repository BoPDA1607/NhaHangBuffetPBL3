using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NhaHangBuffetPBL3.DataAccess.Data;
using NhaHangBuffetPBL3.Models;
using NhaHangBuffetPBL3.Repository.IRepository;

namespace NhaHangBuffetPBL3.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin", Policy = "AdminPolicy")]
    public class IngredientManageController : Controller
    {
        private readonly IUnitOfWork _context;

        public IngredientManageController(IUnitOfWork context)
        {
            _context = context;
        }

        // GET: Admin/NguyenLieu
        public async Task<IActionResult> Index()
        {
            return View(_context.Ingredient.GetAll());
        }

        // GET: Admin/NguyenLieu/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nguyenLieu = _context.Ingredient
                .GetFirstOrDefault(m => m.Ingredient_id == id);
            if (nguyenLieu == null)
            {
                return NotFound();
            }

            return View(nguyenLieu);
        }

        // GET: Admin/NguyenLieu/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/NguyenLieu/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Ingredient_id,Ingredient_name,Stored_quantity,Order_quantity,Unit")] Ingredient nguyenLieu)
        {
            if (ModelState.IsValid)
            {
                if(_context.Ingredient.GetFirstOrDefault(p=>p.Ingredient_id==nguyenLieu.Ingredient_id)!=null)
                {
                    ModelState.AddModelError("Error", "Bị trùng mã món!!");
                    return View();
                }
                _context.Ingredient.Add(nguyenLieu);
                _context.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(nguyenLieu);
        }

        // GET: Admin/NguyenLieu/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nguyenLieu = _context.Ingredient.Find(id);
            if (nguyenLieu == null)
            {
                return NotFound();
            }
            return View(nguyenLieu);
        }

        // POST: Admin/NguyenLieu/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Ingredient_Id,Ingredient_name,Stored_quantity,Order_quantity,Unit")] Ingredient nguyenLieu)
        {
            nguyenLieu.Ingredient_id = id;

            try
            {
                _context.Ingredient.Update(nguyenLieu);
                _context.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NguyenLieuExists(nguyenLieu.Ingredient_id))
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

        // GET: Admin/NguyenLieu/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nguyenLieu = _context.Ingredient
                .GetFirstOrDefault(m => m.Ingredient_id == id);
            if (nguyenLieu == null)
            {
                return NotFound();
            }

            return View(nguyenLieu);
        }

        // POST: Admin/NguyenLieu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var nguyenLieu = _context.Ingredient.Find(id);
            if (nguyenLieu != null)
            {
                _context.Ingredient.Remove(nguyenLieu);
            }

            _context.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool NguyenLieuExists(string id)
        {
            return _context.Ingredient.Any(e => e.Ingredient_id == id);
        }
    }
}
