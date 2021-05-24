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


        public async Task CreateVehicleModel(VehicleModel vehicleModel)
        {
            _context.Add(vehicleModel);
            await _context.SaveChangesAsync();
        }

        public async Task EditVehicleModel(int id, VehicleModel vehicleModel)
        {
            _context.Update(vehicleModel);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteVehicleModel(int id)
        {
            var vehicleModel = await _context.VehicleModels
                .FirstOrDefaultAsync(m => m.Id == id);
            _context.VehicleModels.Remove(vehicleModel);
            await _context.SaveChangesAsync();

        }
    }
}