using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GarageMVC.Data;
using GarageMVC.Models;
using NuGet.Packaging.Signing;

namespace GarageMVC.Controllers
{
    public class GarageController : Controller
    {
        private readonly GarageContext _context;

        public GarageController(GarageContext context)
        {
            _context = context;
        }

        // GET: Garage
        public async Task<IActionResult> Index()
        {
            return View(await _context.ParkedVehicles.ToListAsync());
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
        public IActionResult Create()
        {
            return View();
        }

        // POST: Garage/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Type,Color,RegistrationNumber,Brand,Model,NumberOfWheels")] ParkedVehicleModel parkedVehicleModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(parkedVehicleModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(parkedVehicleModel);
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

        // GET: Garage/CheckOut/5
        public async Task<IActionResult> CheckOut(int? id)
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

            // Getting current time to calculate tot time
            DateTime currentTime = DateTime.Now;

            TimeSpan ParkedDuration = currentTime.Subtract(parkedVehicleModel.TimeStamp);
            string tot = "";


            int hours = ParkedDuration.Hours;
            int minutes = ParkedDuration.Minutes;


            if (hours == 0 && minutes > 0)
            {
                tot = string.Format( minutes + " minutes");
            }
            else if (hours > 0)
            {
                tot = string.Format("{1:00} hours & {1:00} minutes", hours, minutes);
            }


            parkedVehicleModel.ParkedDuration = tot;

            return View(parkedVehicleModel);

        }

        // POST: Garage/CheckOut/5
        [HttpPost, ActionName("CheckOut")]
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
