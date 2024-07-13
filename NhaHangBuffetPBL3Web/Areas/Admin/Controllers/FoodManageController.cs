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
    public class FoodManageController : Controller
    {
        private readonly IUnitOfWork _context;

        public FoodManageController(IUnitOfWork context)
        {
            _context = context;
        }

        // GET: Admin/MonAns
        public async Task<IActionResult> Index()
        {
            return View(_context.Food.GetAll());
        }

        // GET: Admin/MonAns/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var monAn = _context.Food
                .GetFirstOrDefault(m => m.FoodId == id);
            if (monAn == null)
            {
                return NotFound();
            }

            return View(monAn);
        }

        // GET: Admin/MonAns/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/MonAns/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FoodId,FoodName,Type,Image_Url")] Food monAn)
        {
            if (ModelState.IsValid)
            {
                _context.Food.Add(monAn);
                _context.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(monAn);
        }

        // GET: Admin/MonAns/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var monAn = _context.Food.Find(id);
            if (monAn == null)
            {
                return NotFound();
            }
            return View(monAn);
        }

        // POST: Admin/MonAns/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FoodId,FoodName,Type,Image_Url")] Food monAn)
        {
            if (id != monAn.FoodId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Food.Update(monAn);
                    _context.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MonAnExists(monAn.FoodId))
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
            return View(monAn);
        }

        // GET: Admin/MonAns/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var monAn = _context.Food
                .GetFirstOrDefault(m => m.FoodId == id);
            if (monAn == null)
            {
                return NotFound();
            }

            return View(monAn);
        }

        // POST: Admin/MonAns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var monAn = _context.Food.Find(id);
            if (monAn != null)
            {
                _context.Food.Remove(monAn);
            }

            _context.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool MonAnExists(int id)
        {
            return _context.Food.Any(e => e.FoodId == id);
        }
    }
}
