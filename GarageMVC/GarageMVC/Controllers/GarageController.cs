using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GarageMVC.Data;
using GarageMVC.Models;
using GarageMVC.ViewModels;

namespace GarageMVC.Controllers
{
    public class GarageController : Controller
    {
        private readonly GarageContext _context;
        private readonly VehicleConstants _constants;

        public GarageController(GarageContext context, IConfiguration configuration)
        {
            _context = context;
            _constants = new VehicleConstants(configuration);
                //(configuration);     // For use with /Garage/Create
        }

        // GET: Garage
        public async Task<IActionResult> Index()
        {
            IEnumerable<VehicleOverviewViewModel> vehicleOverviews = await _context.ParkedVehicles.Select(pv => new VehicleOverviewViewModel(pv)).ToListAsync();
            return View(new OverviewPageViewModel() { PlacesRemaining = 0, VehicleOverviews = vehicleOverviews });
        }

        // GET: Garage/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkedVehicleModel = await _context.ParkedVehicles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parkedVehicleModel == null)
            {
                return NotFound();
            }

            return View(parkedVehicleModel);
        }

        // GET: Garage/Create
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.VehicleConstants = _constants;
            return View();
        }

        [HttpPost]
        public IActionResult Create(ParkedVehicleModel vehicle)
        {
            if (_context.ParkedVehicles.Any(v => v.RegistrationNumber == vehicle.RegistrationNumber))
            {
                ModelState.AddModelError("RegistrationNumber", "Registration number must be unique");
            }

            if (ModelState.IsValid)
            {
                vehicle.TimeStamp = DateTime.Now;
                _context.ParkedVehicles.Add(vehicle);
                _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.VehicleConstants = _constants;
            return View(vehicle);
        }

        // Used for client side validation in conjunction with Remote attribute on ParkedVehicleModel
        [HttpGet]
        public async Task<IActionResult> CheckRegistration(string registrationNumber)
        {
            if (await _context.ParkedVehicles.AnyAsync(v => v.RegistrationNumber == registrationNumber))
            {
                return Json($"{registrationNumber} has already been taken.");
            }
            
            return Json(true);
        }

        // GET: Garage/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkedVehicleModel = await _context.ParkedVehicles.FindAsync(id);
            if (parkedVehicleModel == null)
            {
                return NotFound();
            }
            return View(parkedVehicleModel);
        }

        // POST: Garage/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Type,Color,RegistrationNumber,Brand,Model,NumberOfWheels")] ParkedVehicleModel parkedVehicleModel)
        {
            if (id != parkedVehicleModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(parkedVehicleModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParkedVehicleModelExists(parkedVehicleModel.Id))
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
            return View(parkedVehicleModel);
        }

        // GET: Garage/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkedVehicleModel = await _context.ParkedVehicles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parkedVehicleModel == null)
            {
                return NotFound();
            }

            return View(parkedVehicleModel);
        }

        // POST: Garage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var parkedVehicleModel = await _context.ParkedVehicles.FindAsync(id);
            if (parkedVehicleModel != null)
            {
                _context.ParkedVehicles.Remove(parkedVehicleModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParkedVehicleModelExists(int id)
        {
            return _context.ParkedVehicles.Any(e => e.Id == id);
        }
    }
}
