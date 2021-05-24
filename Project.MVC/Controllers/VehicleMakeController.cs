using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project.Service;
using Project.Service.Models;
using Project.Service.Services;

namespace Project.MVC.Controllers
{
    public class VehicleMakeController : Controller
    {
        private readonly MyContext _context;

        public IVehicleMakeService VehicleMakeService { get; set; }
        public IVehicleModelService VehicleModelService { get; set; }


        public VehicleMakeController(MyContext context, IVehicleMakeService vehicleMakeService)
        {
            VehicleMakeService = vehicleMakeService;

            _context = context;
        }

        // GET: VehicleMake
        public async Task<IActionResult> Index(
        string sortOrder,
        string currentFilter,
        string searchString,
        int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["Name"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["Abrv"] = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var vehicleMake = from s in _context.VehicleMakes
                              select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                vehicleMake = vehicleMake.Where(s => s.Name.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    vehicleMake = vehicleMake.OrderByDescending(s => s.Name);
                    break;
                case "Date":
                    vehicleMake = vehicleMake.OrderBy(s => s.Abrv);
                    break;
                case "date_desc":
                    vehicleMake = vehicleMake.OrderByDescending(s => s.Name);
                    break;
                default:
                    vehicleMake = vehicleMake.OrderBy(s => s.Abrv);
                    break;
            }
            int pageSize = 3;
            return View(await PaginatedList<VehicleMake>.CreateAsync(vehicleMake.AsNoTracking(), pageNumber ?? 1, pageSize));
            //return View(await vehicleMake.AsNoTracking().ToListAsync());
        }

        //public async Task<IActionResult> Index(string searchString)
        //{
        //    var vehicleMake = from m in _context.VehicleMakes
        //                      select m;

        //    if (!String.IsNullOrEmpty(searchString))
        //    {
        //        vehicleMake = vehicleMake.Where(s => s.Name.Contains(searchString));
        //    }

        //    return View(await vehicleMake.ToListAsync());
        //}


        // GET: VehicleMake/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleMake = await _context.VehicleMakes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicleMake == null)
            {
                return NotFound();
            }

            return View(vehicleMake);
        }

        // GET: VehicleMake/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VehicleMake/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Abrv")] VehicleMake vehicleMake) // <-   UI
        {
            //is valid 
            if (ModelState.IsValid)
            {
                await VehicleMakeService.CreateVehicleMake(vehicleMake);
                return RedirectToAction(nameof(Index));
            }
            return View(vehicleMake);
        }

        // GET: VehicleMake/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleMake = await _context.VehicleMakes.FindAsync(id);
            if (vehicleMake == null)
            {
                return NotFound();
            }
            return View(vehicleMake);
        }

        // POST: VehicleMake/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Abrv")] VehicleMake vehicleMake)
        {
            if (id != vehicleMake.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await VehicleMakeService.EditVehicleMake(id, vehicleMake);
                return RedirectToAction(nameof(Index));
            }
            return View(vehicleMake);
        }

        // GET: VehicleMake/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleMake = await _context.VehicleMakes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicleMake == null)
            {
                return NotFound();
            }

            return View(vehicleMake);
        }

        //POST: VehicleMake/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            await VehicleMakeService.DeleteVehicleMake(id);
            return RedirectToAction(nameof(Index));
        }

        private bool VehicleMakeExists(int id)
        {
            return _context.VehicleMakes.Any(e => e.Id == id);
        }

    }
}