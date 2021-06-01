using Microsoft.EntityFrameworkCore;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Service;


namespace Project.Service.Services
{
    public class VehicleMakeService : IVehicleMakeService
    {
        public MyContext _context { get; set; }
        public VehicleMakeService(MyContext context)
        {
            _context = context;
        }


        public async Task CreateVehicleMakeAsync(VehicleMake vehicleMake)
        {
            _context.Add(vehicleMake);
            await _context.SaveChangesAsync();
        }

        public async Task EditVehicleMakeAsync(int id, VehicleMake vehicleMake)
        {
            _context.Update(vehicleMake);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteVehicleMakeAsync(int id)
        {
            var vehicleMake = await _context.VehicleMakes
                .FirstOrDefaultAsync(m => m.Id == id);
            _context.VehicleMakes.Remove(vehicleMake);
            await _context.SaveChangesAsync();

        }

        public async Task<PaginatedList<VehicleMake>> VehicleMakePagingAsync(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
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

            return await PaginatedList<VehicleMake>.CreateAsync(vehicleMake.AsNoTracking(), pageNumber ?? 1, pageSize);
        }
        public async Task<VehicleMake> FindVehicleMakeAsync(int? id)
        {
            var vehicleMake = await _context.VehicleMakes.FirstOrDefaultAsync(m => m.Id == id);
            return vehicleMake;
        }

        public async Task<List<VehicleMake>> GetVehicleMakesAsync()
        {
            var vehicleMake = await _context.VehicleMakes.ToListAsync();
            return vehicleMake;
        }
    }
}