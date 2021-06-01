using Microsoft.EntityFrameworkCore;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Services
{
    public class VehicleModelService : IVehicleModelService
    {
        public MyContext _context { get; set; }
        public VehicleModelService(MyContext context)
        {
            _context = context;
        }


        public async Task CreateVehicleModelAsync(VehicleModel vehicleModel)
        {
            _context.Add(vehicleModel);
            await _context.SaveChangesAsync();
        }

        public async Task EditVehicleModelAsync(int id, VehicleModel vehicleModel)
        {
            _context.Update(vehicleModel);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteVehicleModelAsync(int id)
        {
            var vehicleModel = await _context.VehicleModels
                .FirstOrDefaultAsync(m => m.Id == id);
            _context.VehicleModels.Remove(vehicleModel);
            await _context.SaveChangesAsync();

        }
        public async Task<PaginatedList<VehicleModel>> VehicleModelPagingAsync(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            var vehicleModel = from s in _context.VehicleModels
                              select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                vehicleModel = vehicleModel.Where(s => s.Name.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    vehicleModel = vehicleModel.OrderByDescending(s => s.Name);
                    break;
                case "Date":
                    vehicleModel = vehicleModel.OrderBy(s => s.Abrv);
                    break;
                case "date_desc":
                    vehicleModel = vehicleModel.OrderByDescending(s => s.Name);
                    break;
                default:
                    vehicleModel = vehicleModel.OrderBy(s => s.Abrv);
                    break;
            }
            int pageSize = 3;

            return await PaginatedList<VehicleModel>.CreateAsync(vehicleModel.AsNoTracking(), pageNumber ?? 1, pageSize);
        }
        public async Task<VehicleModel> FindVehicleModelAsync(int? id)
        {
            var vehicleModel = await _context.VehicleModels.FirstOrDefaultAsync(m => m.Id == id);
            return vehicleModel;
        }
    }
}