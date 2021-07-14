using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project.MVC.Models;
using Project.Service;
using Project.Service.Models;
using Project.Service.Services;

namespace Project.MVC.Controllers
{
    public class VehicleModelController : Controller
    {
        private readonly IMapper Mapper;

        public IVehicleMakeService VehicleMakeService { get; set; }
        public IVehicleModelService VehicleModelService { get; set; }


        public VehicleModelController(IMapper mapper, IVehicleModelService vehicleModelService, IVehicleMakeService vehicleMakeService)
        {
            VehicleModelService = vehicleModelService;
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

            PaginatedList<Project.Service.Models.VehicleModel> vehicleMakePagedList = await VehicleModelService.VehicleModelPagingAsync(sortOrder, currentFilter, searchString, pageNumber);
            return View(vehicleMakePagedList);

        }


            // GET: VehicleModel/Details/5
            public async Task<IActionResult> DetailsAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleModel = await VehicleModelService.FindVehicleModelAsync(id);
            if (vehicleModel == null)
            {
                return NotFound();
            }

            return View(vehicleModel);
        }

        // GET: VehicleModel/Create
        public async Task<IActionResult> CreateAsync()
        {
            CreateVehicleModel createVehicleModel = new CreateVehicleModel();
            List<SelectListItem> selectListItems = new List<SelectListItem>();
            List<Project.Service.Models.VehicleMake> vehicleMakes = await VehicleMakeService.GetVehicleMakesAsync();
            foreach (var vehicleMake in vehicleMakes)
            {

                
            }
            List<Project.MVC.Models.VehicleMake> mappedVehicleMakes = Mapper.Map<List<Project.Service.Models.VehicleMake>, List<Project.MVC.Models.VehicleMake>>(vehicleMakes);
            foreach (var vehicleMake in mappedVehicleMakes)
            {

            }

            foreach (var vehicleMake in vehicleMakes)
            {

                var selectListItem = new SelectListItem
                {
                    Value = vehicleMake.Id.ToString(),
                    Text = vehicleMake.Name
                };
                selectListItems.Add(selectListItem);
            }
            createVehicleModel.VehicleMakes = selectListItems;
            foreach (var vehicleMake in createVehicleModel.VehicleMakes)
            {

            }

            return View(createVehicleModel);
        }

        // POST: VehicleMake/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync([Bind("Id,Name,Abrv")] Project.MVC.Models.VehicleModel vehicleModel) 
        {
            
            //is valid 
            if (ModelState.IsValid) 
            {
                //await VehicleModelService.CreateVehicleModelAsync(vehicleModel);
                //return RedirectToAction(nameof(Index));
                Project.Service.Models.VehicleModel vehicleModelModel = Mapper.Map<Project.MVC.Models.VehicleModel, Project.Service.Models.VehicleModel>(vehicleModel);
                await VehicleModelService.CreateVehicleModelAsync(vehicleModelModel);
                return RedirectToAction(nameof(Index));
            }
            return View(vehicleModel);
        }

        // GET: VehicleMake/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleModel = await VehicleModelService.FindVehicleModelAsync(id);
            if (vehicleModel == null)
            {
                return NotFound();
            }
            return View(vehicleModel);
        }

        // POST: VehicleMake/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(int id, [Bind("Id,Name,Abrv")] Project.MVC.Models.VehicleModel vehicleModel)
        {
            if (id != vehicleModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                Project.Service.Models.VehicleModel vehicleModelModel = Mapper.Map<Project.MVC.Models.VehicleModel, Project.Service.Models.VehicleModel>(vehicleModel);
                await VehicleModelService.EditVehicleModelAsync(id, vehicleModelModel);
                return RedirectToAction(nameof(Index));
            }
            return View(vehicleModel);
        }

        // GET: VehicleMake/Delete/5
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await VehicleModelService.DeleteVehicleModelAsync(id);
            return RedirectToAction(nameof(Index));
        }


    }
}