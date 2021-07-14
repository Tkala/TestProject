using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project.Service;
using Project.Service.Models;
using Project.Service.NewFolder;
using Project.Service.Services;

namespace Project.MVC.Controllers
{
    public class VehicleMakeController : Controller
    {
        private readonly IMapper Mapper;

        public IVehicleMakeService VehicleMakeService { get; set; }
        public IVehicleModelService VehicleModelService { get; set; }


        public VehicleMakeController(IMapper mapper, IVehicleMakeService vehicleMakeService)
        {
            VehicleMakeService = vehicleMakeService;
            Mapper = mapper;

        }

        // GET: VehicleMake
        public async Task<IActionResult> IndexAsync(
        string sortOrder,
        string currentFilter,
        string searchString,
        int? pageNumber)
        {

            //PaginatedList p = service.Paging(sortOrder, currentFilter, searchString, pageNumber);
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


            PaginatedList<VehicleMake> vehicleMakePagedList = await VehicleMakeService.VehicleMakePagingAsync(sortOrder, currentFilter, searchString, pageNumber);

            return View(vehicleMakePagedList);
            //return View(await vehicleMake.AsNoTracking().ToListAsync());
        }
        //    public async Task<ActionResult> IndexAsync(string orderBy, string searchString, string currentFilter, bool ascending = true, int pageNumber = 0, int pageSize = 0)
        //{
        //    ViewBag.currentSort = orderBy;
        //    ViewBag.currentAscending = ascending;
        //    ViewBag.nameSortParam = "name";
        //    ViewBag.abrvSortParam = "abrv";
        //    ViewBag.vehicleMakeSortParam = "vehicleMake";
        //    ViewBag.ascending = ascending.ToString().ToLower() == "false" ? "true" : "false";

        //    List<ISortingPair> sortingParametersList = new List<ISortingPair>();
        //    sortingParametersList.Add(SortingPair.CreateSortingPair(ascending, orderBy));

        //    if(searchString != null)
        //    {
        //        pageNumber = 1;
        //    }
        //    else
        //    {
        //        searchString = currentFilter;
        //    }
        //    ViewBag.currentFilter = searchString;

        //    IPagingParameters pagingParameters = PagingParameters.CreatePagingParameters(pageNumber, pageSize);
        //    ISortingParameters sortingParameters = SortingParameters.CreateSortingParameters(sortingParametersList);
        //    IFilter filter = filter.CreateFilter(searchString);

        //    var vehicleMakes = await Service.GetAsync(pagingParameters, sortingParameters, filter);

        //    return View(vehicleMakes);
        //}
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
        public async Task<IActionResult> DetailsAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var vehicleMake = await VehicleMakeService.FindVehicleMakeAsync(id);
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
        public async Task<IActionResult> CreateAsync([Bind("Id,Name,Abrv")] Project.MVC.Models.VehicleMake vehicleMake) // <-   UI
        {
            //is valid 
            if (ModelState.IsValid)
            {
                Project.Service.Models.VehicleMake vehicleMakeModel = Mapper.Map<Project.MVC.Models.VehicleMake, Project.Service.Models.VehicleMake>(vehicleMake);
                await VehicleMakeService.CreateVehicleMakeAsync(vehicleMakeModel);
                return RedirectToAction(nameof(Index));
            }
            return View(vehicleMake);
        }

        // GET: VehicleMake/Edit/5
        public async Task<IActionResult> EditAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleMake = await VehicleMakeService.FindVehicleMakeAsync(id);
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
        public async Task<IActionResult> EditAsync(int id, [Bind("Id,Name,Abrv")] Project.MVC.Models.VehicleMake vehicleMake)
        {
            
            if (id != vehicleMake.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                Project.Service.Models.VehicleMake vehicleMakeModel = Mapper.Map<Project.MVC.Models.VehicleMake, Project.Service.Models.VehicleMake>(vehicleMake);
                await VehicleMakeService.EditVehicleMakeAsync(id, vehicleMakeModel);
                return RedirectToAction(nameof(Index));
            }
            return View(vehicleMake);
        }

        // GET: VehicleMake/Delete/5
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await VehicleMakeService.DeleteVehicleMakeAsync(id);
            return RedirectToAction(nameof(Index));
        }

        //POST: VehicleMake/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{

        //    await VehicleMakeService.DeleteVehicleMake(id);
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool VehicleMakeExists(int id)
        //{
        //    return _context.VehicleMakes.Any(e => e.Id == id);
        //}

    }
}