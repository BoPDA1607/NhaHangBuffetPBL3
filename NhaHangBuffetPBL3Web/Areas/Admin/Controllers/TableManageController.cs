using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NhaHangBuffetPBL3.DataAccess.Data;
using NhaHangBuffetPBL3.Models;
using NhaHangBuffetPBL3.Repository;
using NhaHangBuffetPBL3.Repository.IRepository;

namespace NhaHangBuffetPBL3.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles="Admin",Policy="AdminPolicy")]
    public class TableManageController : Controller
    {
        private readonly IUnitOfWork _context;

        public TableManageController(IUnitOfWork context)
        {
            _context = context;
        }

        // GET: Admin/BanAn
        public async Task<IActionResult> Index()
        {
            var banan = _context.Table.GetAll();
            return View(banan);
        }

        // GET: Admin/BanAn/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var banAn = _context.Table
                .GetFirstOrDefault(m => m.SeatingId == id);
            if (banAn == null)
            {
                return NotFound();
            }

            return View(banAn);
        }
        public IActionResult Create()
        {
            using (var _context1 = new NhaHangBuffetContext())
            {
                var newSeatingId = _context.Table.GetAll().Count()+1; 

                // Begin transaction
                using var transaction = _context1.Database.BeginTransaction();
                try
                {
                    // Set IDENTITY_INSERT to ON
                    _context1.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [Tables] ON");

                    // Insert the new record with the specific ID
                    var banan = new Table
                    {
                        SeatingId = newSeatingId,
                    };
                    _context1.Tables.Add(banan);
                    _context1.SaveChanges();

                    // Set IDENTITY_INSERT to OFF
                    _context1.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [Tables] OFF");

                    // Commit transaction
                    transaction.Commit();

                    Console.WriteLine("New record added successfully with SeatingId: " + newSeatingId);
                }
                catch (Exception ex)
                {
                    // Rollback transaction on error
                    transaction.Rollback();
                    Console.WriteLine("Error adding record: " + ex.Message);

                }
                return RedirectToAction("Index");
            }
        }

        // GET: Admin/BanAn/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var banAn = _context.Table.Find(id);
            if (banAn == null)
            {
                return NotFound();
            }
            return View(banAn);
        }

        // POST: Admin/BanAn/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SeatingId,SeatingDate,NumberOfPeople,Status")] Table banAn)
        {
            if (id != banAn.SeatingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Table.Update(banAn);
                    _context.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BanAnExists(banAn.SeatingId))
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
            return View(banAn);
        }

        // GET: Admin/BanAn/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var banAn = _context.Table
                .GetFirstOrDefault(m => m.SeatingId == id);
            if (banAn == null)
            {
                return NotFound();
            }

            return View(banAn);
        }

        // POST: Admin/BanAn/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var banAn = _context.Table.Find(id);
            if (banAn != null)
            {
                _context.Table.Remove(banAn);
            }

            _context.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool BanAnExists(int id)
        {
            return _context.Table.Any(e => e.SeatingId == id);
        }
    }
}
